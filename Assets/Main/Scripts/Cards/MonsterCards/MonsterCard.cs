using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterCard", menuName = "Cards/MonsterCard")]
public class MonsterCard : ScriptableObject, ICard
{
    public string monster;
    public string region;
    public string evolveText;
    public string[] evolveHits;

    public string Title => monster;
}
