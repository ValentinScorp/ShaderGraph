public class IncreaseHeroStrength : IPlayerEffect
{
    private int _amount;
    public IncreaseHeroStrength(int amount) {
        _amount = amount;
    }
    public void Apply(GameContext context, Player player) {
        player.Hero.IncreaseStrength(_amount);
    }
}