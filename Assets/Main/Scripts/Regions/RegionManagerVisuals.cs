using UnityEngine;

public class RegionManagerVisuals : MonoBehaviour
{
    public void PlaceGameObjectAt(RegionId regionId, GameObject gameObject) {
        Transform regionTransform = transform.Find(RegionIdParser.ToName(regionId));

        if (regionTransform == null) {
            Debug.LogError($"Region '{regionId}' not found under '{name}'!");
            return;
        }

        gameObject.transform.SetParent(regionTransform, worldPositionStays: true);
    }
}
