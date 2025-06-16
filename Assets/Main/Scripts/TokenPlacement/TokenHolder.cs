using System.Collections.Generic;
using UnityEngine;


public class TokenHolder
{
    private readonly List<TokenPlacementInfo> _placedTokens;
    private GameObject _currentObject = null;
    private TokenType _tokenType = TokenType.None;
    public bool HasObject => _currentObject != null;
    public TokenType TokenType => _tokenType;

    public List<TokenPlacementInfo> PlacedTokens => _placedTokens;

    public GameObject CurrentObject => _currentObject;

    public TokenHolder() {
        _placedTokens = new();
    }
    
    public Vector3 GetTokenPosition() {
        return _currentObject.transform.position;
    }
    public void SetTokenPosition(Vector3 newPosition) {
        _currentObject.transform.position = newPosition;
    }
    public void InitPalcement(GameObject gameObject, TokenType tokenType) {
        _currentObject = gameObject;
        _tokenType = tokenType;
    }
    public void Reset() {
        _placedTokens.Clear();
    }
    public void DestroyTokens() {
        foreach (var token in _placedTokens) {
            if (token.TokenObject != null) {
                Object.Destroy(token.TokenObject);
            }
        }
        _placedTokens.Clear();
    }
    public void CacheCurrentToken(PlayerColor color, RegionId regionId) {
        _placedTokens.Add(new TokenPlacementInfo(_currentObject, color, regionId, _tokenType));
    }
    public void OnCancelPlacing() {
        DestroyObject();
        DestroyTokens();
        Reset();
    }
    public void ReleaseToken() {
        if(HasObject) {
            _currentObject = null;
            _tokenType = TokenType.None;
        } else {
            Debug.LogWarning("Unable to place empty object!");
        }
    }
    public void DestroyObject() {
        if (HasObject) {
            Object.Destroy(_currentObject);
            _currentObject = null;
            _tokenType = TokenType.None;
        } else {
            Debug.LogWarning("Unable to Destroy empty object!");
        }
    }
}
