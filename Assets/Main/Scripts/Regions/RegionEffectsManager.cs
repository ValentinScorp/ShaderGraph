using UnityEngine;
using UnityEngine.EventSystems;

public class RegionEffectsManager : MonoBehaviour
{
    [SerializeField] private RegionId _regionId;
    [SerializeField] private Renderer _regionBorder;
    [SerializeField] private RegionInfoUiPanel _regionInfoUiIPanel;

    private RegionBorderEmissionController _emissionController;
    private bool _isSelected = false;

    public RegionId RegionId => _regionId;

    private void Awake() {
        if (_regionBorder == null) {
            Debug.LogError("Error getting target object in RegionSelectManager!", gameObject);
            return;
        }
        _emissionController = _regionBorder.GetComponent<RegionBorderEmissionController>();
        if (_emissionController == null) {
            Debug.LogError("Error getting EmissionPulseController component of target object in RegionSelectManager!", gameObject);
        }
    }

    private void OnMouseEnter() {
        if (!_isSelected) { 
            _emissionController?.EnablePulse();
        }
    }
    private void OnMouseExit() {
        if (!_isSelected) {
            _emissionController?.DisablePulse();
        }
    }

    public void Activate() {
        _isSelected = true;
        _emissionController?.SetEmissionMax();
    }
    public void Deactivate() {
        _isSelected = false;
        _emissionController?.SetEmissionMin();
    }
}
