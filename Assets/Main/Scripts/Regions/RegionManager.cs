using System;
using System.Collections.Generic;
using UnityEngine;
using static Board;

[DisallowMultipleComponent]
public class RegionManager : MonoBehaviour
{
    public TextAsset JsonFile;
    private RegionJsonWrapper RegionJsonWrapper { get; set; } = new();
    [SerializeField] private List<RegionRuntimeData> _regionList = new();
    private readonly Dictionary<RegionId, RegionRuntimeData> _regionMap = new();
    //private GameContext _gameContext;

    private void Awake() {
        LoadFromJson();
        BuildGraph();
    }

    private void LoadFromJson() {
        try {
            RegionJsonWrapper = JsonUtility.FromJson<RegionJsonWrapper>(JsonFile.text);
        } catch (Exception ex) {
            string message = $"Error on {gameObject.name}: {ex.Message}";
            Debug.LogError(message, gameObject);
        }

        foreach (var jsonRegion in RegionJsonWrapper.regions) {
            RegionData regionData = new ();
            CopyRegionData(jsonRegion, regionData);
            RegionRuntimeData regionRuntimeData = new (regionData);
            _regionList.Add(regionRuntimeData);
        }

        foreach (var region in _regionList) {
            _regionMap[region.RegionId] = region;
        }
    }
    /*
    public void Initialize(GameContext context) {
        _gameContext = context;
    }
    */
    private void BuildGraph() {
        foreach (RegionRuntimeData region in _regionList) {
            region.RegionData.Neighbors = new List<RegionConnection>();

            foreach (string neighborName in region.RegionData.SourceData.neighbors_land) {
                if (_regionMap.TryGetValue(RegionIdParser.Parse(neighborName), out RegionRuntimeData neighbor)) {
                    region.RegionData.Neighbors.Add(new RegionConnection {
                        TargetRegionId = neighbor.RegionId,
                        ConnectionType = RegionConnectionType.Land
                    });
                } else {
                    Debug.LogWarning($"Land neighbor '{neighborName}' not found for region '{region.RegionData.RegionName}'");
                }
            }

            foreach (string seaNeighborName in region.RegionData.SourceData.neighbors_sea) {
                if (_regionMap.TryGetValue(RegionIdParser.Parse(seaNeighborName), out RegionRuntimeData neighbor)) {
                    region.RegionData.Neighbors.Add(new RegionConnection {
                        TargetRegionId = neighbor.RegionId,
                        ConnectionType = RegionConnectionType.Sea
                    });
                } else {
                    Debug.LogWarning($"Sea neighbor '{seaNeighborName}' not found for region '{region.RegionData.RegionName}'");
                }
            }
        }
    }

    private void CopyRegionData(RegionJson source, RegionData dest) {
        dest.CopyData(source);
    }

    public RegionRuntimeData GetRegionData(RegionId regionId) {
        if (_regionMap.TryGetValue(regionId, out RegionRuntimeData regionData)) {
            return regionData;
        }
        Debug.LogWarning($"Region with ID {regionId} not found in _regionMap");
        return null;
    }

    
    /*public void AddToken(string regionName, IRegionTokenView tokenView) {
        var region = _regions.Find(r => r.Name == regionName);
        var view = _regionViews.Find(v => v.RegionName == regionName);
        if (region != null && view != null) {
            region.AddToken(tokenView.Model);
            view.AddTokenView(tokenView);
        }
    }*/ 

    public RegionRuntimeData FindRegion(RegionId regionName) {
        var region = _regionList.Find(r => r.RegionId == regionName);
        if (region == null) {
            Debug.LogError("Error at {RegionManager} {PlaceHopliteAt} {region == null}!");
            return null;
        }
        return region;
    } 

    public bool PlaceHopliteAt(RegionId regionId, PlayerColor playerColor) {
        var region = FindRegion(regionId);
        if (region == null) {
            return false;            
        }
        var hoplite = new Hoplite(playerColor);
        region.AddHoplite(hoplite);
        return true;
    }
    public bool PlaceHeroAt(RegionId regionId, Hero hero) {
        var region = FindRegion(regionId);
        if (region == null) {
            return false;
        }
        region.AddHero(hero);
        return true;
    }
}
