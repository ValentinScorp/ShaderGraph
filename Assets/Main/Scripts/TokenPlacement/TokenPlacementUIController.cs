using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class TokenPlacementUIController
{
    private readonly GameObject _panel;
    private readonly ButtonActionBinder _buttonActionBinder = new();
    public event Action<TokenType> OnStartPlacement;
    public event Action OnFinalizePlacement;
    public event Action OnCancelPlacement;
    public void ShowPanel(bool show) => _panel.SetActive(show);

    public TokenPlacementUIController(GameObject panel) {
        _panel = panel ?? throw new ArgumentNullException(nameof(panel));
        BindButtons();
        ShowPanel(false);
    }

    private void BindButtons() {
        BindButton("PlaceHero_Button", () => OnStartPlacement?.Invoke(TokenType.Hero));
        BindButton("PlaceHoplite_Button", () => OnStartPlacement?.Invoke(TokenType.Hoplite));
        BindButton("OkPlacing_Button", () => OnFinalizePlacement?.Invoke());
        BindButton("CancelCreateHoplite_Button", () => OnCancelPlacement?.Invoke());
    }

    public void BindButton(string buttonName, Action callback) {
        if (callback == null) {
            Debug.LogWarning($"Button '{buttonName}' callback is null!");
            return;
        }

        foreach (var button in _panel.GetComponentsInChildren<Button>(true)) {
            if (button.name == buttonName) {
                _buttonActionBinder.Bind(button, callback);
                return;
            }           
        }
        Debug.LogWarning("Couldn`t find button: " + buttonName);
    }
    public void UpdateButtonInteractability(TokenPlacementTracker placementTracker) {
        SetButtonInteractable("PlaceHero_Button", placementTracker.CanPlace(TokenType.Hero));
        SetButtonInteractable("PlaceHoplite_Button", placementTracker.CanPlace(TokenType.Hoplite));
        SetButtonInteractable("OkPlacing_Button", placementTracker.AllPlaced);
    }
    public void SetButtonInteractable(string buttonName, bool interactable) {
        _buttonActionBinder.InteractableButtonByButtonName(buttonName, interactable);
    }
    public void UnbindAllButtons() {
        _buttonActionBinder.UnbindAll();
    }
}
