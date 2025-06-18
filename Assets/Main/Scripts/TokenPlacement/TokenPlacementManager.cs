
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TokenPlacementManager : MonoBehaviour
{
    [SerializeField] private RegionManager _regionManager;
    [SerializeField] private ColorPalette _colorPalette;
    [SerializeField] private TokenFactory _tokenFactory;
    [SerializeField] private GameObject _boardSurface;
    [SerializeField] private GameObject _tokenPlacementControl_Panel;
    private readonly TokenPlacementTracker _tokenPlacementTracker = new();

    [SerializeField] private UserInputController _userInputController;

    private TokenHolder _tokenHolder;
    private TokenVisualChanger _tokenVisualChanger;
    private TokenPlacementUIController _uiController = null;

    private TokenPlacementValidator _tokenPlacementValidator;
    public event Action<List<TokenPlacementInfo>> OnPlacementFinished;

    private Player _player;

    public float Radius = 1f;

    private int _boardLayerMask;
    private RaycastIntersector _raycastIntersector;

    void Awake() {
        Debug.Log("TokenPlacementManager Awake, new UiController");

        _boardLayerMask = 1 << LayerMask.NameToLayer("BoardSurface");
        if (_uiController == null) {
            _uiController = new TokenPlacementUIController(_tokenPlacementControl_Panel);
        }

        _tokenHolder = new TokenHolder();
        _tokenVisualChanger = new TokenVisualChanger(_colorPalette);
        _tokenPlacementValidator = new TokenPlacementValidator(_regionManager);
        _raycastIntersector = new RaycastIntersector(Camera.main, _boardSurface, _boardLayerMask);

        _tokenFactory = new TokenFactory();
    }

    private void OnEnable() {
        Debug.Log("TokenPlacementManager OnEnable");

        _userInputController.OnMouseClick += HandleMouseClick;
        _uiController.OnStartPlacement += StartPlacingToken;
        _uiController.OnFinalizePlacement += FinalizePlacement;
        _uiController.OnCancelPlacement += CancelPlacement;
    }

    public void InitiatePlacing(Player player, int heroesCount, int hoplitesCount) {
        Debug.Log("TokenPlacementManager InitiatePlacing");

        _player = player;
        _uiController.ShowPanel(true);
        _tokenPlacementTracker.SetPlacementTargets(heroesCount, hoplitesCount);
        _uiController.UpdateButtonInteractability(_tokenPlacementTracker);
    }

    private void OnDisable() {
        Debug.Log("TokenPlacementManager OnDisable");

        _userInputController.OnMouseClick -= HandleMouseClick;
        _uiController.OnStartPlacement -= StartPlacingToken;
        _uiController.OnFinalizePlacement -= FinalizePlacement;
        _uiController.OnCancelPlacement -= CancelPlacement;
    }

    
    private void HandleMouseClick() {
        if (!_raycastIntersector.IsPointerOverUI()) {
            ReleaseObject();
        }
    }
    private void StartPlacingToken(TokenType tokenType) {
        if (!_tokenHolder.HasObject) {
            var token = _tokenFactory.CreateToken(tokenType);
            
            _tokenVisualChanger.SetGhostMaterial(token);
            _tokenHolder.InitPalcement(token, tokenType);

            float radius = _tokenFactory.GetRadius(tokenType);
            _tokenPlacementValidator.SetTokenRadius(radius);
        }
    }
    private void FinalizePlacement() {
        if (_tokenHolder?.HasObject == true) {
            _tokenHolder.DestroyObject();
        }

        _uiController?.ShowPanel(false);
        OnPlacementFinished?.Invoke(_tokenHolder.PlacedTokens);
    }
    private void CancelPlacement() {
        _tokenHolder.OnCancelPlacing();

        _uiController.ShowPanel(true);
        _tokenPlacementTracker.Reset();
        _uiController.UpdateButtonInteractability(_tokenPlacementTracker);
    }
    void Update() {
        if (!_tokenHolder.HasObject) {
            return;
        }
        if (_raycastIntersector.TryGetBoardPosition(Input.mousePosition, out Vector3 newPosition)) {
            if (_tokenHolder.GetTokenPosition() != newPosition) {
                _tokenHolder.SetTokenPosition(newPosition);

                var state = _tokenPlacementValidator.ValidatePlacement(newPosition);
                _tokenVisualChanger.SetTokenMaterialGhostState(_tokenHolder.CurrentObject, state);                
            }
        }
    }    
    public void ReleaseObject() {
        if (_tokenHolder.HasObject && _tokenPlacementValidator.IsCurrentStateAllowed) {
            var position = _tokenHolder.GetTokenPosition();
            if (_tokenPlacementValidator.TryGetRegionIdAtPosition(position, out RegionId regionId)) {
                _tokenPlacementTracker.CountToken(_tokenHolder.TokenType);
                _tokenVisualChanger.PrepareTokenPlacement(_tokenHolder.CurrentObject, _player.Color);
                _tokenHolder.CacheCurrentToken(_player.Color, regionId);
                _tokenHolder.ReleaseToken();
                _uiController.UpdateButtonInteractability(_tokenPlacementTracker);
            }
        } else {
            Debug.Log("Can't place");
        }
    }

    private void OnDestroy() {
        Debug.Log("TokenPlacementManager OnDestroy");
        _uiController.UnbindAllButtons();
        //_uiController = null;
    }
}
