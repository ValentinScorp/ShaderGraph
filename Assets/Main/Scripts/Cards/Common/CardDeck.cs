using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDeck
{
    private List<ICard> _deck = new();
    private List<ICard> _discardPile = new();

    public CardDeck(IEnumerable<ICard> cards) {
        _deck = cards.ToList();
        Shuffle();
    }

    public void Shuffle() {
        for (int i = 0; i < _deck.Count; i++) {
            int rand = UnityEngine.Random.Range(i, _deck.Count);
            (_deck[i], _deck[rand]) = (_deck[rand], _deck[i]);
        }
    }

    public ICard Draw() {
        if (_deck.Count == 0) {
            if (_discardPile.Count == 0) {
                Debug.LogWarning("No cards left to draw.");
                return null;
            }

            _deck.AddRange(_discardPile);
            _discardPile.Clear();
            Shuffle();
        }

        var card = _deck[0];
        _deck.RemoveAt(0);
        return card;
    }

    public void Discard(ICard card) {
        if (card != null)
            _discardPile.Add(card);
    }

    public List<ICard> DrawMultiple(int count) {
        var drawn = new List<ICard>();
        for (int i = 0; i < count; i++) {
            var card = Draw();
            if (card == null) break;
            drawn.Add(card);
        }
        return drawn;
    }

    public int DeckCount => _deck.Count;
    public int DiscardCount => _discardPile.Count;
}
