using UnityEngine;

[System.Serializable]
public class Hero
{
    [SerializeField] private string _name;
    [SerializeField] private PlayerColor _playerColor;    
    private HeroConfig _heroData;
    public PlayerColor PlayerColor => _playerColor;
    public string Name => _name;
    public Hero(string name, PlayerColor playerColor) {
        _name = name;
        _playerColor = playerColor;
    }
    public void IncreaseStrength(int strengthDelta) {
        _heroData.SetStrength(_heroData.Strength + strengthDelta);
    }
}
