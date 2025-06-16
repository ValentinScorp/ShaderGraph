using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class RegionRuntimeData
{
    public RegionId RegionId { get; private set; }
    public RegionData RegionData { get; private set; }
    public bool HasTemple { get; set; }
    public PlayerColor? ControlledBy { get; set; }
    public List<Quest> ActiveQuests { get; private set; } = new();
    public bool IsFortified { get; private set; }

    [SerializeField] private List<Hero> _heroes = new();
    [SerializeField] private List<Monster> _monsters = new();
    [SerializeField] private List<Hoplite> _hoplites = new();

    public IReadOnlyList<Hero> Heroes => _heroes;
    public IReadOnlyList<Monster> Monsters => _monsters;
    public IReadOnlyList<Hoplite> Hoplites => _hoplites;

    public RegionRuntimeData(RegionData regionData) {
        RegionId = RegionIdParser.Parse(regionData.RegionName);
        RegionData = regionData;
    }
    public void AddHero(Hero hero) {
        if (!_heroes.Contains(hero)) {
            _heroes.Add(hero);
        }
    }
    public void RemoveHero(Hero hero) {
        _heroes.Remove(hero);
    }
    public void AddMonster(Monster monster) {
        if (!_monsters.Contains(monster)) {
            _monsters.Add(monster);
        }
    }
    public void RemoveMonster(Monster monster) {
        _monsters.Remove(monster);
    }
    public void AddHoplite(Hoplite hoplite) {
        if (!_hoplites.Contains(hoplite)) {
            _hoplites.Add(hoplite);
        }
    }
    public void RemoveHoplite(Hoplite hoplite) {
        _hoplites.Remove(hoplite);
    }
    public int GetHopliteCount(PlayerColor color) {
        return _hoplites.FindAll(h => h.PlayerColor == color).Count;
    }
 }
