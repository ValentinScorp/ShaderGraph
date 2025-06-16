using System.Collections.Generic;
using UnityEngine;
using static Board;

[System.Serializable]
public class GameContext : MonoBehaviour
{
    // Players


    // Cards
    [SerializeField] private CardDeck _eventDeck;
    [SerializeField] private CardDeck _artifactDeck;
    [SerializeField] private CardDeck _blessingDeck;
    [SerializeField] private CardDeck _templeDeck;

    // GamePlay
    [SerializeField] private RegionManager _regionManager;    
    [SerializeField] private GameManager _gameManager;

    public CardDeck EventDeck => _eventDeck;
    public CardDeck ArtifactDeck => _artifactDeck;
    public CardDeck BlessingDeck => _blessingDeck;

    public GameContext(RegionManager regionManager) {
        _regionManager = regionManager;
    }

    public void InitializeDecks(CardLoader cardLoader) {
        List<ICard> eventCards = new List<ICard>();
        eventCards.AddRange(cardLoader.MonsterCards);
        eventCards.AddRange(cardLoader.QuestCards);
        _eventDeck = new CardDeck(eventCards);

        _artifactDeck = new CardDeck(cardLoader.ArtifactCards);
        _blessingDeck = new CardDeck(cardLoader.BlessingCards);
        _templeDeck = new CardDeck(cardLoader.TempleCards);
    }
}
