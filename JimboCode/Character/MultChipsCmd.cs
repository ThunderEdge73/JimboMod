using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Character;

public static class MultChipsCmd
{
    public static void MultiplyMult(Player player, int mult)
    {
        SetMult(player, GetMult(player) * mult);
    }

    public static void AddMult(Player player, int mult)
    {
        SetMult(player, GetMult(player) + mult);
    }

    public static void AddChips(Player player, int chips)
    {
        SetChips(player, GetChips(player) + chips);
    }
    
    public static int GetChips(Player player)
    {
        return player.PlayerCombatState == null ? 0 : MultChipsPointsSingleton.Chips.Get(player.PlayerCombatState);
    }

    public static void SetChips(Player player, int chips)
    {
        if (player.PlayerCombatState == null) return;
        MultChipsPointsSingleton.Chips.Set(player.PlayerCombatState, chips);
    }

    public static int GetMult(Player player)
    {
        return player.PlayerCombatState == null ? 0 : MultChipsPointsSingleton.Mult.Get(player.PlayerCombatState);
    }

    public static void SetMult(Player player, int mult)
    {
        if (player.PlayerCombatState == null) return;
        MultChipsPointsSingleton.Mult.Set(player.PlayerCombatState, mult);
    }

    public static int GetPoints(Player player)
    {
        return player.PlayerCombatState == null ? 0 : MultChipsPointsSingleton.Points.Get(player.PlayerCombatState);
    }

    public static int CalculatePointsEarned(Player player)
    {
        if (player.PlayerCombatState == null) return 0;
        return MultChipsPointsSingleton.Chips.Get(player.PlayerCombatState) * MultChipsPointsSingleton.Mult.Get(player.PlayerCombatState);
    }
}

public class MultChipsPointsSingleton() : CustomSingletonModel(true, false)
{
    public static readonly SpireField<PlayerCombatState, int> Chips = new(() => 0);

    public static readonly SpireField<PlayerCombatState, int> Mult = new(() => 0);

    public static readonly SpireField<PlayerCombatState, int> Points = new(() => 0);

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.IsScore() || cardPlay.Card.Owner.PlayerCombatState == null) return Task.CompletedTask;
        var player = cardPlay.Card.Owner;
        var totalPoints = MultChipsCmd.CalculatePointsEarned(player) + MultChipsCmd.GetPoints(player);
        Points.Set(player.PlayerCombatState, totalPoints);
        Chips.Set(player.PlayerCombatState, 0);
        Mult.Set(player.PlayerCombatState, 0);
        return Task.CompletedTask;
    }

    
}