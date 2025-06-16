using UnityEngine;

[System.Serializable]
public class Hoplite
{
    [SerializeField] private PlayerColor _playerColor;
    public PlayerColor PlayerColor => _playerColor;

    public Hoplite(PlayerColor playerColor) {
        _playerColor = playerColor;
    }
}
