using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager
{
    [SerializeField] private Player _activePlayer = null;
    [SerializeField] private List<Player> _players = new();
    public Player ActivePlayer => _activePlayer;
    public int PlayerCount => _players.Count;

    private int _currentPlayerIndex = 0;
    public void AddPlayer(Player player) {
        if (_players.Contains(player)) {
            Debug.LogWarning($"Player {player.Color} with hero {player.Hero.Name} is already added.");
            return;
        }
        _players.Add(player);

        if (_players.Count == 1) {
            _currentPlayerIndex = 0;
            _activePlayer = player;
        }
    }
    public void SetActivePlayer(PlayerColor playerColor) {
        var activePlayer = _players.Find(ap => ap.Color == playerColor);
        if (activePlayer != null) {
            _activePlayer = activePlayer;
            _currentPlayerIndex = _players.IndexOf(activePlayer); 
            Debug.Log($"Setting active player {_activePlayer.Color} (index {_currentPlayerIndex}) in PlayerTurnManager");
        } else {
            Debug.LogWarning("Can't set active player at {GameContext} player {PlayerColor} not found!");
        }
    }
    public Player GetActivePlayer() {
        return _activePlayer;
    }
    public void NextPlayer() {
        if (_players.Count == 0) {
            return;
        }

        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        _activePlayer = _players[_currentPlayerIndex];
        Debug.Log($"Next player is {_activePlayer.Color}");
    }

    public void PrevPlayer() {
        if (_players.Count == 0) {
            return;
        }

        _currentPlayerIndex = (_currentPlayerIndex - 1 + _players.Count) % _players.Count;
        _activePlayer = _players[_currentPlayerIndex];
        Debug.Log($"Prev player is {_activePlayer.Color}");

    }
}
