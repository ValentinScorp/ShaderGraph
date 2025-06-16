using UnityEngine;

public class TokenPlacementTracker
{
    private int _totalHoplites;
    private int _totalHeroes;
    private int _placedHoplites;
    private int _placedHeroes;

    public void SetPlacementTargets(int heroesCount, int hoplitesCount) {
        _totalHeroes = heroesCount;
        _totalHoplites = hoplitesCount;
        _placedHeroes = 0;
        _placedHoplites = 0;
    }
    public void Reset() {
        _placedHoplites = 0;
        _placedHeroes = 0;
    }
    public void CountToken(TokenType? type) {
        if (type == TokenType.Hoplite) {
            _placedHoplites++;
        }
        if (type == TokenType.Hero) {
            _placedHeroes++;
        }
    }
    public bool CanPlace(TokenType type) {
        return type switch {
            TokenType.Hoplite => _placedHoplites < _totalHoplites,
            TokenType.Hero => _placedHeroes < _totalHeroes,
            _ => false
        };
    }
    public bool AllPlaced =>
        _placedHeroes >= _totalHeroes && _placedHoplites >= _totalHoplites;

    public int PlacedHoplites => _placedHoplites;
    public int PlacedHeroes => _placedHeroes;
    public int TotalHoplites => _totalHoplites;
    public int TotalHeroes => _totalHeroes;
}
