using UnityEngine;

[CreateAssetMenu(fileName = "NewCombatCard", menuName = "Cards/CombatCard")]
public class CombatCard : ScriptableObject, ICard
{
	public string tacticName;
    public string type;
    public int value;
    public string use;
	public string effect;
    public int casualties;
    public bool singlePlayer;
    public bool expansionCard;

    public string Title => name;
}
