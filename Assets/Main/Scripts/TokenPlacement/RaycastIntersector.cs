using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RaycastIntersector
{
    private readonly Camera _mainCamera;
    private readonly int _boardLayerMask;
    private readonly GameObject _boardSurface;

    public RaycastIntersector(Camera mainCamera, GameObject boardSurface, int boardLayerMask) {
        _mainCamera = mainCamera;
        _boardSurface = boardSurface;
        _boardLayerMask = boardLayerMask;
    }
    public bool TryGetBoardPosition(Vector3 mousePosition, out Vector3 position) {
        position = Vector3.zero;
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _boardLayerMask) && hit.collider.gameObject == _boardSurface) {
            position = hit.point;
            return true;
        }
        return false;
    }

    public bool IsPointerOverUI() {
        if (EventSystem.current == null) {
            Debug.LogWarning("EventSystem is not present in the scene.");
            return false;
        }

        PointerEventData eventData = new PointerEventData(EventSystem.current) {
            position = Mouse.current.position.ReadValue()
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        int transparentLayer = LayerMask.NameToLayer("RaycastHitTransparent");

        foreach (RaycastResult result in results) {
            if (result.gameObject.layer != transparentLayer) {
                return true;
            }
        }

        return false;
    }
}
