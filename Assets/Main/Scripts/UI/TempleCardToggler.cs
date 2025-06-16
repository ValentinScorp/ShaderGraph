using UnityEngine;
using UnityEngine.UI;

public class TempleCardToggler : MonoBehaviour
{
    [Header("UI References")]
    public Button toggleButton;
    public Animator buttonsAnimator;
    public CanvasGroup buttonsGroup;

    private bool isVisible = false;

    private void Start() {
        toggleButton.onClick.AddListener(Toggle);
    }

    private void Toggle() {
        isVisible = !isVisible;
        buttonsAnimator.SetTrigger("Toggle");
    }
    public void EnableInteraction() {
        buttonsGroup.interactable = true;
        buttonsGroup.blocksRaycasts = true;
    }

    public void DisableInteraction() {
        buttonsGroup.interactable = false;
        buttonsGroup.blocksRaycasts = false;
    }
}
