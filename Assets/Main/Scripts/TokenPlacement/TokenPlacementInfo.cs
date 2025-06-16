using UnityEngine;

public class TokenPlacementInfo
{
    private GameObject _tokenObject;
    private PlayerColor _playerColor;
    private RegionId _regionId;
    private TokenType? _tokenType;
    
    public GameObject TokenObject => _tokenObject;
    public PlayerColor PlayerColor => _playerColor;
    public RegionId RegionId => _regionId;
    public TokenType? TokenType => _tokenType;

    public TokenPlacementInfo(GameObject obj, PlayerColor playerColor, RegionId regionId, TokenType? type) {
        _tokenObject = obj;
        _playerColor = playerColor;
        _regionId = regionId;
        _tokenType = type;
    }
}
