using UnityEngine;
using UnityEngine.UI;
using TMPro;  // ќсь тут ≥мпортуЇмо TextMeshPro

public class TemplePool : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _cardInfoText;
    [SerializeField] private TextMeshProUGUI _draftInfoText;
    [SerializeField] private Button _randomCardButton;
    [SerializeField] private Button _nextStepButton;

    private CardLoader _cardLoader;
    private TempleCard _currentCard;
    private int _currentStep = 0;
    private string _currentStepText;

    private void Awake() {
        _cardLoader = GetComponent<CardLoader>(); 
    }

    void Start() {
        _randomCardButton.onClick.AddListener(SelectRandomCard);
        _nextStepButton.onClick.AddListener(AdvanceStep);

        UpdateUI();
    }

    void SelectRandomCard() {
        if (_cardLoader.TempleCards == null || _cardLoader.TempleCards.Count == 0) {
            _cardInfoText.text = "No cards available";
            _draftInfoText.text = "";
            return;
        }
        _currentCard = _cardLoader.TempleCards[Random.Range(0, _cardLoader.TempleCards.Count)] ;
        _currentStep = 0;
        _currentStepText = $"Draft Step {_currentStep + 1}: {_currentCard.drafts[_currentStep]}";
        UpdateUI();
    }

    void AdvanceStep() {
        if (_currentCard != null) {
            if (_currentStep == _currentCard.drafts.Length - 1) {
                _currentStepText = $"Out of temples";

            } else {
                _currentStep = Mathf.Min(_currentStep + 1, _currentCard.drafts.Length - 1);
                _currentStepText = $"Draft Step {_currentStep + 1}: {_currentCard.drafts[_currentStep]}";
            }
        }
        UpdateUI();
    }

    void UpdateUI() {
        if (_currentCard == null) {
            _cardInfoText.text = "No card selected";
            _draftInfoText.text = "";
            return;
        }

        _cardInfoText.text = $"Oracle Blessing: {_currentCard.oracleBlessing}";
        _draftInfoText.text = _currentStepText;
    }
}
