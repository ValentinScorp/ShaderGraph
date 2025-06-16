using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Scriptable Objects/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    public Color ghostColorInit;
    public Color ghostColorError;
    public Color ghostColorOk;

    public Material ghostMaterial;
    public Material greyTokenMaterial;
    public Material greenTokenMaterial;
    public Material blueTokenMaterial;
    public Material redTokenMaterial;
    public Material yellowTokenMaterial;
}
