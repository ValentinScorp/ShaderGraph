using UnityEngine;

public class GamePhaseHeroesPlacement
{
    private PlayerTurnManager _playerTurnManager;
    private int _playersToPlace = 0;
    private bool _isStarted = false;

    public GamePhaseHeroesPlacement(PlayerTurnManager playerTurnManager) {
        _playerTurnManager = playerTurnManager;
        _playersToPlace = _playerTurnManager.PlayerCount;
    }
    public void SetFirstPlayer(PlayerColor color) {
        if (_isStarted) {
            Debug.LogWarning("Can't set first player. Hero placement phase already started.");
            return;
        }
        _playerTurnManager.SetActivePlayer(color);
        _isStarted = true;
    }
    public Player GetActivePlayer() {
        return _playerTurnManager.GetActivePlayer();
    }
    public Player GetNextPlayer() {
        if (_playersToPlace <= 1) {
            return null; 
        }

        _playerTurnManager.PrevPlayer();
        _playersToPlace--;
        return _playerTurnManager.GetActivePlayer();
    }
    public bool IsPlacementFinished() {
        return _playersToPlace <= 1;
    }
}
