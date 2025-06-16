using System.Linq;
using TMPro;
using UnityEngine;

public class TokenPrefabVisual : MonoBehaviour
{
    [SerializeField] private TokenType _tokenType;

    private Renderer _renderer = null;
    private Transform _visual = null;
    private Transform _canvas = null;
       
    public TokenType TokenType => _tokenType;

    private void Awake() {
        _visual = transform.Find("Visual");
        _renderer = _visual?.GetComponent<Renderer>();
        _canvas = transform.Find("Canvas");

        if (!_visual) {
            Debug.LogError("Can't get Visual!");
            Debug.LogError($"Class: {GetType().Name}");
            Debug.LogError($"Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        }
        if (!_renderer) {
            Debug.LogError("Can't get Renderer from Visual!");
            Debug.LogError($"Class: {GetType().Name}");
            Debug.LogError($"Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        }
               
        if (!_canvas) {
            Debug.LogError("Can't get Canvas!");
            Debug.LogError($"Class: {GetType().Name}");
            Debug.LogError($"Method: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
        }
    }

    public void SetVisualLayer(string layerName) {
        if (_visual == null) return;

        int layer = LayerMask.NameToLayer(layerName);
        if (layer < 0) {
            Debug.LogError($"Layer '{layerName}' not found.");
            return;
        }
        _visual.gameObject.layer = LayerMask.NameToLayer(layerName);
        /*
        foreach (Transform child in _visual.GetComponentsInChildren<Transform>(true)) {
            if (child.GetComponent<TMPro.TextMeshProUGUI>() == null) {
                child.gameObject.layer = layer;
            }
        }*/
    }

    public void SetVisualTag(string tagName) {
        if (_visual == null) {
            Debug.LogWarning("Visual not assigned.");
            return;
        }
        // TODO remove these from runtime build
        if (!UnityEditorInternal.InternalEditorUtility.tags.Contains(tagName)) {
            Debug.LogWarning($"Tag '{tagName}' does not exist in project settings.");
        }

        _visual.tag = tagName;
    }

    public void MakeInvisible() {
        _renderer.enabled = false;
    }
    public void MakeVisible() {
        _renderer.enabled = true;
    }

    public void SetLabel(string text) {
        if (_canvas == null) {
            Debug.LogWarning("Canvas is null — cannot set label.");
            return;
        }

        var label = _canvas.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        if (label != null) {
            label.text = text;
        } else {
            Debug.LogWarning("TextMeshProUGUI not found under canvas.");
        }
    }
}
