using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _regions;

    [SerializeField] private Button _startPlacement_Button;
    [SerializeField] private GameObject _tokenPlacementOnStart_Panel;

    private GameContext _gameContext;
    private PlayerTurnManager _playerTurnManager = new();
    private GamePhaseHeroesPlacement _gamePhaseHeroesPlacement;
    private RegionManager _regionManager;
    private RegionManagerVisuals _regionVisuals;
    private TokenPlacementManager _tokenPlacementManager;
    private GameLog _gameLog;
    private TemplePool _templePool;
    public RegionManager RegionManager => _regionManager;

    private void Awake() {
        //Debug.Log("GameManager Awake");

        _gameContext = GetComponent<GameContext>();
        _regionManager = _regions.GetComponent<RegionManager>();
        _regionVisuals = _regions.GetComponent<RegionManagerVisuals>();
        _tokenPlacementManager = GetComponent<TokenPlacementManager>();
        _gameLog = GetComponent<GameLog>();

        if (_startPlacement_Button != null) {
            _startPlacement_Button.onClick.AddListener(EnablePlacementUI);
        }
    }

    private void Start() {
        //Debug.Log("GameManager Start");

        _gameContext.InitializeDecks(GetComponent<CardLoader>());

        Player player1 = new Player(PlayerColor.Blue, new Hero("Achilles", PlayerColor.Blue));
        Player player2 = new Player(PlayerColor.Red, new Hero("Heracles", PlayerColor.Red));

        _playerTurnManager.AddPlayer(player1);
        _playerTurnManager.AddPlayer(player2);


        _gamePhaseHeroesPlacement = new(_playerTurnManager);


        _tokenPlacementManager.OnPlacementFinished += HandlePlacementFinished;
        DisablePlacementUI();

    }

    private void EnablePlacementUI() {
        _tokenPlacementOnStart_Panel.SetActive(true);
        _startPlacement_Button.interactable = false;
        _tokenPlacementManager.InitiatePlacing(_gamePhaseHeroesPlacement.GetActivePlayer(), 1, 2);
    }

    private void DisablePlacementUI() {
        _tokenPlacementOnStart_Panel.SetActive(false);
        _startPlacement_Button.interactable = true;
    }
    public bool TryRegisterHopliteAt(TokenPlacementInfo tokenInfo) {
        if (tokenInfo.RegionId != RegionId.Unknown) {
            _regionManager.PlaceHopliteAt(tokenInfo.RegionId, tokenInfo.PlayerColor);
            _regionVisuals.PlaceGameObjectAt(tokenInfo.RegionId, tokenInfo.TokenObject);

            _gameLog.AddLogEntry($"Addin Hoplite to {tokenInfo.RegionId}");
            return true;
        } else {
            Debug.LogWarning("Error in {GameManager} {RegisterHopliteAt}");
        }
        return false;
    }
    public bool TryRegisterHeroAt(TokenPlacementInfo tokenInfo) {
        if (tokenInfo.RegionId != RegionId.Unknown) {
            _regionManager.PlaceHeroAt(tokenInfo.RegionId, new Hero("Hero_name", tokenInfo.PlayerColor));
            _regionVisuals.PlaceGameObjectAt(tokenInfo.RegionId, tokenInfo.TokenObject);

            _gameLog.AddLogEntry($"Addin Hoplite to {tokenInfo.RegionId}");
            return true;
        } else {
            Debug.LogWarning("Error in {GameManager} {RegisterHopliteAt}");
        }
        Debug.Log(RegionIdParser.ToName(tokenInfo.RegionId));
        return false;
    }
    private void HandlePlacementFinished(List<TokenPlacementInfo> placedTokens) {
        foreach (var token in placedTokens) {
            //Debug.Log($"Token of type {token.TokenType} placed in region {token.RegionId}");
            switch (token.TokenType) {
                case TokenType.Hoplite:
                    TryRegisterHopliteAt(token);                     
                    break;
                case TokenType.Hero:
                    TryRegisterHeroAt(token);               
                    break;
                default:
                    Debug.LogWarning("Unknown token type!");
                    break;
            }


            // _gameContext.RegisterTokenPlacement(token); // або аналог≥чна лог≥ка
            // TODO
        }

        Player nextPlayer = _gamePhaseHeroesPlacement.GetNextPlayer();
        if (nextPlayer != null) {
            _tokenPlacementManager.InitiatePlacing(nextPlayer, 1, 2);
        } else {

            _gamePhaseHeroesPlacement = null;
            _tokenPlacementManager.enabled = false;
            StartGameLoop();
        }
    }

    private void StartGameLoop() {
        Debug.Log($"Game loop started. First turn: {_playerTurnManager.ActivePlayer.Color}");

        // встановити пор€док ход≥в (напр€мок Ч вперед в≥д останнього)
        //
        // TODO


        // TODO: викликати перший х≥д
    }

    private void OnValidate() {
        var all = UnityEngine.Object.FindObjectsByType<GameManager>(
            FindObjectsSortMode.None
        );

        if (all.Length > 1) {
            Debug.LogError("GameManager is assigned to multiple objects. Only one is allowed.");
        }
    }

    private void OnDestroy() {
        //Debug.Log("GameManager OnDestroy");

        if (_startPlacement_Button != null) {
            _startPlacement_Button.onClick.RemoveListener(EnablePlacementUI);
        }
        if (_tokenPlacementManager != null) {
            _tokenPlacementManager.OnPlacementFinished -= HandlePlacementFinished;
        }
    }
}