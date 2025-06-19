using UnityEngine;
using UnityEngine.UI;
using System;

public class TokenPlacementUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject panel;

    [SerializeField] private Button placeHeroButton;
    [SerializeField] private Button placeHopliteButton;
    [SerializeField] private Button okPlacingButton;
    [SerializeField] private Button cancelButton;

    public event Action<TokenType> OnStartPlacement;
    public event Action OnFinalizePlacement;
    public event Action OnCancelPlacement;

    private void Awake() {
        if (placeHeroButton != null)
            placeHeroButton.onClick.AddListener(() => OnStartPlacement?.Invoke(TokenType.Hero));

        if (placeHopliteButton != null)
            placeHopliteButton.onClick.AddListener(() => OnStartPlacement?.Invoke(TokenType.Hoplite));

        if (okPlacingButton != null)
            okPlacingButton.onClick.AddListener(() => OnFinalizePlacement?.Invoke());

        if (cancelButton != null)
            cancelButton.onClick.AddListener(() => OnCancelPlacement?.Invoke());

        ShowPanel(false); // start hidden
    }

    public void ShowPanel(bool show) {
        if (panel != null)
            panel.SetActive(show);
    }

    public void UpdateButtonInteractability(TokenPlacementTracker tracker) {
        placeHeroButton.interactable = tracker.CanPlace(TokenType.Hero);
        placeHopliteButton.interactable = tracker.CanPlace(TokenType.Hoplite);
        okPlacingButton.interactable = tracker.AllPlaced;
    }

    public void UnbindAllButtons() {
        placeHeroButton?.onClick.RemoveAllListeners();
        placeHopliteButton?.onClick.RemoveAllListeners();
        okPlacingButton?.onClick.RemoveAllListeners();
        cancelButton?.onClick.RemoveAllListeners();
    }
}
