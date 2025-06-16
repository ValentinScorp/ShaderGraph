using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestCard", menuName = "Cards/QuestCard")]
public class QuestCard : ScriptableObject, ICard
{
    public string quest;
    public string region;
    public string[] stepsCondition;
    public string reward;

    public string Title => quest;
}
