using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "NewArtifactCard", menuName = "Cards/ArtifactCard")]
public class ArtifactCard : ScriptableObject, ICard
{
    public string titleName;
    public string effect;
    public string owner;
    public bool neutral;

    public string Title => titleName;
}
