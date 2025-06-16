using UnityEngine;

[CreateAssetMenu(fileName = "HeroConfig", menuName = "Game/HeroConfig")]
public class HeroConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _leadership = 1;
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _strength = 1;
    [SerializeField] private IPlayerEffect _startingBonus;
    [SerializeField] private IPlayerEffect _specialAbility;

    public string Name => _name;
    public int Leadership => _leadership;
    public int Speed => _speed;
    public int Strength => _strength;
    public IPlayerEffect StartingBonus => _startingBonus;
    public IPlayerEffect SpecialAbility => _specialAbility;

    public void SetStrength(int value) {
        _strength = value;
    }
}
