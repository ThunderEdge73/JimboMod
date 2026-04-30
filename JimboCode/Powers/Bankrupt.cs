using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Jimbo.JimboCode.Powers;

public class Bankrupt : JimboPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target,
        DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (Owner.CombatState == null || target != Owner || result.UnblockedDamage <= 0 ||
            Owner.CombatState.CurrentSide != Owner.Side)
            return;
        await PowerCmd.Remove(this);
    }

    public override async Task AfterRemoved(Creature oldOwner)
    {
        if (Owner.Player == null) return;
        await PlayerCmd.LoseGold(Math.Min(Owner.Player.Gold, Amount), Owner.Player);
    }
}