using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Powers;

public class PointsPower : JimboPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCurrentHpChanged(Creature creature, decimal delta)
    {
        if (creature.Player != null && delta < 0)
        {
            await PowerCmd.ModifyAmount(new ThrowingPlayerChoiceContext(), this, Math.Floor(-Amount / (decimal) 2),
                creature,
                null);
        }
    }
}