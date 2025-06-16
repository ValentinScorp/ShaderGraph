using UnityEditor.Rendering;
using UnityEngine;

public class TokenVisualChanger
{
    private readonly TokenMaterialChanger _tokenMaterialChanger;

    public TokenVisualChanger(ColorPalette colorPalette) {
        _tokenMaterialChanger = new TokenMaterialChanger(colorPalette);
    }

    public void SetGhostMaterial(GameObject gameObject) {
        _tokenMaterialChanger.SetGhostMaterial(gameObject);
    }
    public void SetTokenMaterialGhostState(GameObject gameObject, TokenPlacementValidator.GhostState state) {
        _tokenMaterialChanger.SetStateColor(gameObject, state);
    }

    public void PrepareTokenPlacement(GameObject gameObject, PlayerColor playerColor) {
        _tokenMaterialChanger.SetPlayerMaterial(gameObject, playerColor);

        gameObject.GetComponent<TokenPrefabVisual>()?.SetVisualLayer("HoplonToken");
        gameObject.GetComponent<TokenPrefabVisual>()?.SetVisualTag("PlacedToken");

        if (gameObject.TryGetComponent<Rigidbody>(out var rb)) {
            Object.Destroy(rb);
        } else {
            Debug.LogError($"No Rigidbody found in: {gameObject.name}");
        }
    }

    public void SetGhostState(GameObject tokenObject, TokenPlacementValidator.GhostState state) {
        _tokenMaterialChanger.SetStateColor(tokenObject, state);
    }

    public void ApplyPlacementVisuals(GameObject tokenObject, PlayerColor playerColor) {
        _tokenMaterialChanger.SetPlayerMaterial(tokenObject, playerColor);

        var visual = tokenObject.GetComponent<TokenPrefabVisual>();
        visual?.SetVisualLayer("HoplonToken");
        visual?.SetVisualTag("PlacedToken");

        if (tokenObject.TryGetComponent<Rigidbody>(out var rb)) {
            Object.Destroy(rb);
        } else {
            Debug.LogError($"No Rigidbody found in: {tokenObject.name}");
        }
    }
}
