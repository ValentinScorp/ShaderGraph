using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardLoader : MonoBehaviour
{
    [SerializeField] private TextAsset _artifactFileJson;
    [SerializeField] private TextAsset _templeFileJson;
    [SerializeField] private TextAsset _questFileJson;
    [SerializeField] private TextAsset _monsterFileJson;
    [SerializeField] private TextAsset _monsterAttackFileJson;
    [SerializeField] private TextAsset _combatFileJson;    
    [SerializeField] private TextAsset _blessingFileJson;

    [SerializeField] private ArtifactCard[] _artifactCards;
    [SerializeField] private TempleCard[] _templeCards;
    [SerializeField] private QuestCard[] _questCards;
    [SerializeField] private MonsterCard[] _monsterCards;
    [SerializeField] private MonsterAttackCard[] _monsterAttackCards;
    [SerializeField] private CombatCard[] _combatCards;
    [SerializeField] private BlessingCard[] _blessingCards;

    public List<ArtifactCard> ArtifactCards => _artifactCards.ToList();
    public List<TempleCard> TempleCards => _templeCards.ToList();
    public List<QuestCard> QuestCards => _questCards.ToList();
    public List<MonsterCard> MonsterCards => _monsterCards.ToList();
    public List<MonsterAttackCard> MonsterAttackCards => _monsterAttackCards.ToList();
    public List<CombatCard> CombatCards => _combatCards.ToList();
    public List<BlessingCard> BlessingCards => _blessingCards.ToList();

    private void Awake() {
        LoadAllCards();
    }

    private void LoadAllCards() {

        var artifactCardFactory = new CardFactory<ArtifactCard, ArtifactCardJson>(CopyArtifactCardData);
        _artifactCards = artifactCardFactory.LoadCards(_artifactFileJson);

        var templeCardFactory = new CardFactory<TempleCard, TempleCardJson>(CopyTempleCardData);
        _templeCards = templeCardFactory.LoadCards(_templeFileJson);

        var questCardFactory = new CardFactory<QuestCard, QuestCardJson>(CopyQuestCardData);
        _questCards = questCardFactory.LoadCards(_questFileJson);

        var monsterCardFactory = new CardFactory<MonsterCard, MonsterCardJson>(CopyMonsterCardData);
        _monsterCards = monsterCardFactory.LoadCards(_monsterFileJson);

        var monsterAttackCardFactory = new CardFactory<MonsterAttackCard, MonsterAttackCardJson>(CopyMonsterAttackCardData);
        _monsterAttackCards = monsterAttackCardFactory.LoadCards(_monsterAttackFileJson);

        var combatCardFactory = new CardFactory<CombatCard, CombatCardJson>(CopyCombatCardData);
        _combatCards = combatCardFactory.LoadCards(_combatFileJson);

        var blessingCardFactory = new CardFactory<BlessingCard, BlessingCardJson>(CopyBlessingCardData);
        _blessingCards = blessingCardFactory.LoadCards(_blessingFileJson);
/*
        Debug.Log($"Loaded {templeCards.Length} temple cards");
        Debug.Log($"Loaded {questCards.Length} quest cards");
        Debug.Log($"Loaded {monsterCards.Length} monster cards");
        Debug.Log($"Loaded {monsterAttackCards.Length} monster attack cards");
        Debug.Log($"Loaded {combatCards.Length} combat cards");
        Debug.Log($"Loaded {artifactCards.Length} artifact cards");
        Debug.Log($"Loaded {blessingCards.Length} blessing cards");*/
    }

    private void CopyTempleCardData(TempleCardJson sourceJson, TempleCard targetCard) {
        targetCard.drafts = sourceJson.drafts;
        targetCard.oracleBlessing = sourceJson.oracle_blessing;
    }
    private void CopyQuestCardData(QuestCardJson sourceJson, QuestCard targetCard) {
        targetCard.quest = sourceJson.quest;
        targetCard.region = sourceJson.region;
        targetCard.stepsCondition = sourceJson.steps_condition;
        targetCard.reward = sourceJson.reward;
    }
    private void CopyMonsterCardData(MonsterCardJson sourceJson, MonsterCard targetCard) {
        targetCard.monster = sourceJson.monster;
        targetCard.region = sourceJson.region;
        targetCard.evolveText = sourceJson.evolve_text;
        targetCard.evolveHits = sourceJson.evolve_hits;
    }
    private void CopyMonsterAttackCardData(MonsterAttackCardJson sourceJson, MonsterAttackCard targetCard) {
        targetCard.attackName = sourceJson.attack_name;
        targetCard.value = sourceJson.value;
        targetCard.effect = sourceJson.effect;
    }
    private void CopyCombatCardData(CombatCardJson sourceJson, CombatCard targetCard) {
        targetCard.tacticName = sourceJson.tactic_name;
        targetCard.type = sourceJson.type;
        targetCard.value = sourceJson.value;
        targetCard.use = sourceJson.use;
        targetCard.effect = sourceJson.effect;
        targetCard.casualties = sourceJson.casualties;
        targetCard.singlePlayer = sourceJson.single_player;
        targetCard.expansionCard = sourceJson.expansion_card;
    }
    private void CopyArtifactCardData(ArtifactCardJson sourceJson, ArtifactCard targetCard) {
        targetCard.titleName = sourceJson.title_name;
        targetCard.effect = sourceJson.effect;
        targetCard.owner = sourceJson.owner;
        targetCard.neutral = sourceJson.neutral;
    }
    private void CopyBlessingCardData(BlessingCardJson sourceJson, BlessingCard targetCard) {
        targetCard.godName = sourceJson.god_name;
        targetCard.titleName = sourceJson.title_name;
        targetCard.effect = sourceJson.effect;
    }
}