using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField] private PlayerColor _color;
    [SerializeField] private Hero _hero;
    [SerializeField] private int _hoplitesInHand = 15;
    [SerializeField] private int _priestsInHand = 4;
    [SerializeField] private int _priestsInPool = 0;
    [SerializeField] private List<string> _controlledRegions;
    [SerializeField] private List<ArtifactCard> _artifactCards;

    public Hero Hero => _hero;
    public PlayerColor Color => _color;

    public Player(PlayerColor color, Hero hero) {
        _color = color;
        _hero = hero;
    }

    public void AddArtifact(ICard card) {
        if (card is ArtifactCard artifactCard) {
            _artifactCards.Add(artifactCard);
        } else {
            Debug.LogError("Trying to add non-artifac card to player!");
        }
    }
}
