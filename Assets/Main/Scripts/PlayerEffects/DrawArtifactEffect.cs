public class DrawArtifactEffect : IPlayerEffect
{
    private readonly int _drawCount;
    public DrawArtifactEffect(int drawCount) {
        _drawCount = drawCount;
    }
    public void Apply(GameContext context, Player player) {
        var artifacts = context.ArtifactDeck.DrawMultiple(_drawCount);
        // TODO add select dialog
        if (artifacts.Count > 0) {
            player.AddArtifact(artifacts[0]);
        }
    }
}