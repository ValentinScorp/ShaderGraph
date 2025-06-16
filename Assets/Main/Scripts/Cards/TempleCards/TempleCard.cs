using UnityEngine;

[CreateAssetMenu(fileName = "NewTempleCard", menuName = "Cards/TempleCard")]
public class TempleCard : ScriptableObject, ICard
{
    public bool[] drafts;
    public string oracleBlessing;

    public string Title => oracleBlessing;
}
