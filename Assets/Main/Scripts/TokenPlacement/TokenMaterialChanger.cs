using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class TokenMaterialChanger
{
    private ColorPalette _colorPallete;
    public TokenMaterialChanger(ColorPalette colorPalette) {
        _colorPallete = colorPalette;
    }
    public void SetGhostMaterial(GameObject gameObject) {
        GetRenderer(gameObject).material = _colorPallete.ghostMaterial;
    }
    public void SetStateColor(GameObject gameObject, TokenPlacementValidator.GhostState state) {
        switch (state) {
            case TokenPlacementValidator.GhostState.Neutral:
                SetColor(gameObject, _colorPallete.ghostColorInit);
                break;
            case TokenPlacementValidator.GhostState.Allowed:
                SetColor(gameObject, _colorPallete.ghostColorOk);
                break;
            case TokenPlacementValidator.GhostState.Forbidden:
                SetColor(gameObject, _colorPallete.ghostColorError);
                break;
            default:
                SetColor(gameObject, _colorPallete.ghostColorInit);
                break;
        }
    }

    public void SetPlayerMaterial(GameObject gameObject, PlayerColor playerColor) {
        switch (playerColor) {
            case PlayerColor.Red:
                SetMaterial(gameObject, _colorPallete.redTokenMaterial);
                break;
            case PlayerColor.Blue:
                SetMaterial(gameObject, _colorPallete.blueTokenMaterial);
                break;
            case PlayerColor.Green:
                SetMaterial(gameObject, _colorPallete.greenTokenMaterial);
                break;
            case PlayerColor.Yellow:
                SetMaterial(gameObject, _colorPallete.yellowTokenMaterial);
                break;
            default:
                SetMaterial(gameObject, _colorPallete.ghostMaterial);         
                break;
        }
    }

    private Renderer GetRenderer(GameObject gameObject) {
        var renderer = gameObject.GetComponentInChildren<Renderer>();
        return renderer;
    }

    private void SetMaterial(GameObject gameObject, Material material) {
        GetRenderer(gameObject).material = material;
    }

    private void SetColor(GameObject gameObject, Color color) {
        GetRenderer(gameObject).material.SetColor("_FresnelTint", color);
    }
}
