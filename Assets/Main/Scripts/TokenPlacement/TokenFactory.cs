using UnityEngine;
using System;
using System.Collections.Generic;

public class TokenFactory
{
    private readonly TokenLoader _tokenLoader;

    public TokenFactory() {
        _tokenLoader = new TokenLoader("Prefabs/Tokens");
    }

    public GameObject CreateToken(TokenType type) {
        var prefab = _tokenLoader.GetPrefab(type);
        if (prefab == null) {
            Debug.LogError($"[TokenFactory] Can't create token. Prefab missing for type: {type}");
            return null;
        }

        return UnityEngine.Object.Instantiate(prefab);
    }

    public float GetRadius(TokenType type) {
        return _tokenLoader.GetRadius(type);
    }
}
