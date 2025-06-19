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
    public void CacheCurrentToken(PlayerColor color, RegionId regionId) {
        _placedTokens.Add(new TokenPlacementInfo(_currentObject, color, regionId, _tokenType));
    }
    public void OnOkPlacing() {
        DestroyObject();
        ClearPlacedTokensBuffer();
    }
    public void OnCancelPlacing() {
        DestroyObject();
        DestroyPlacedTokens();
        ClearPlacedTokensBuffer();
    }
    public void ReleaseToken() {
        if(HasObject) {
            _currentObject = null;
            _tokenType = TokenType.None;
        } else {
            Debug.LogWarning("Unable to place empty object!");
        }
    }
    private void DestroyObject() {
        if (HasObject) {
            Debug.LogWarning("Destroing object in TokenHolder!");
            Object.Destroy(_currentObject);
            _currentObject = null;
            _tokenType = TokenType.None;
        } 
    }
    private void DestroyPlacedTokens() {
        foreach (var token in _placedTokens) {
            if (token.TokenObject != null) {
                Object.Destroy(token.TokenObject);
            }
        }
        _placedTokens.Clear();
    }
    private void ClearPlacedTokensBuffer() {
        _placedTokens.Clear();
    }
}
