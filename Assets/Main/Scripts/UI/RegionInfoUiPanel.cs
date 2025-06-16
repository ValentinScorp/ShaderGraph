using TMPro;
using UnityEngine;

public class RegionInfoUiPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _regionNameText;
    [SerializeField] private TextMeshProUGUI _populationStrengthText;
    [SerializeField] private TextMeshProUGUI _hoplitesCount;

    public void ShowRegionInfo(RegionRuntimeData regionRuntimeData) {
        _regionNameText.text = $"Region Name: {regionRuntimeData.RegionData.RegionName}";
        _populationStrengthText.text = $"Population Strength: {regionRuntimeData.RegionData.PopulationStrength}";
        _hoplitesCount.text = $"Hoplites Count: {regionRuntimeData.Hoplites.Count}";
        //CityToggle.isOn = data.SourceData.city;
        //ShrineToggle.isOn = data.SourceData.shrine;
        //PortToggle.isOn = data.SourceData.port;
        //MonumentToggle.isOn = data.SourceData.monument;
        gameObject.SetActive(true);
    }
    public void HidePanel() {
        gameObject.SetActive(false);
    }
}
