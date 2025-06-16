using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterAttackCard", menuName = "Cards/MonsterAttackCard")]
public class MonsterAttackCard : ScriptableObject, ICard
{
    public string attackName;
    public string value;
    public string effect;

    public string Title => attackName;
}
