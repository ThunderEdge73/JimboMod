using BaseLib.Abstracts;
using BaseLib.Audio;
using BaseLib.Utils;
using Jimbo.JimboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Character;

public static class MultChipsCmd
{
    public static void MultiplyMult(Player player, decimal mult)
    {
        ModAudio.PlaySound(MainFile.JimboSounds["x_mult.ogg"], 3f);
        SetMult(player, GetMult(player) * mult);
    }

    public static void AddMult(Player player, decimal mult)
    {
        ModAudio.PlaySound(MainFile.JimboSounds["plus_mult.ogg"], 3f);
        SetMult(player, GetMult(player) + mult);
    }

    public static void AddChips(Player player, decimal chips)
    {
        ModAudio.PlaySound(MainFile.JimboSounds["plus_chips.ogg"], 3f);
        SetChips(player, GetChips(player) + chips);
    }

    public static decimal GetChips(Player player)
    {
        return player.PlayerCombatState == null ? 0 : MultChipsPointsSingleton.Chips.Get(player.PlayerCombatState);
    }

    public static void SetChips(Player player, decimal chips)
    {
        if (player.PlayerCombatState == null) return;
        MultChipsPointsSingleton.Chips.Set(player.PlayerCombatState, chips);
        MultChipsUi.UpdateChipsText(player, chips);
    }

    public static decimal GetMult(Player player)
    {
        return player.PlayerCombatState == null ? 0 : MultChipsPointsSingleton.Mult.Get(player.PlayerCombatState);
    }

    public static void SetMult(Player player, decimal mult)
    {
        if (player.PlayerCombatState == null) return;
        MultChipsPointsSingleton.Mult.Set(player.PlayerCombatState, mult);
        MultChipsUi.UpdateMultText(player, mult);
    }

    public static decimal GetPoints(Player player)
    {
        return player.PlayerCombatState == null ? 0 : player.Creature.GetPowerAmount<PointsPower>();
    }

    public static decimal CalculatePointsEarned(Player player)
    {
        if (player.PlayerCombatState == null) return 0;
        return GetChips(player) * GetMult(player);
    }
}

public class MultChipsPointsSingleton() : CustomSingletonModel(true, false)
{
    public static readonly SpireField<PlayerCombatState, decimal> Chips = new(() => 0);

    public static readonly SpireField<PlayerCombatState, decimal> Mult = new(() => 0);

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.IsScore() || cardPlay.Card.Owner.PlayerCombatState == null) return;
        var player = cardPlay.Card.Owner;
        var pts = MultChipsCmd.CalculatePointsEarned(player);
        Chips.Set(player.PlayerCombatState, 0);
        Mult.Set(player.PlayerCombatState, 0);
        ModAudio.PlaySound(MainFile.JimboSounds["mod_score.ogg"], 3f);
        await PowerCmd.Apply<PointsPower>(choiceContext, player.Creature, pts, player.Creature, cardPlay.Card);
    }
}