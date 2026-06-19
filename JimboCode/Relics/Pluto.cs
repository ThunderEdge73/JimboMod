using Jimbo.JimboCode.Character;
using Jimbo.JimboCode.Misc;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace Jimbo.JimboCode.Relics;

public class Pluto : JimboRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new("Mult", 1M), new("Chips", 10M)];

    public override Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!cardPlay.Card.IsScore() || cardPlay.Card.Owner.PlayerCombatState == null) return Task.CompletedTask;
        MultChipsCmd.SetMult(Owner, 1);
        MultChipsCmd.SetChips(Owner, 10);
        return Task.CompletedTask;
    }

    public override Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is not CombatRoom) return Task.CompletedTask;
        MultChipsCmd.SetMult(Owner, 1);
        MultChipsCmd.SetChips(Owner, 10);
        return Task.CompletedTask;
    }
}