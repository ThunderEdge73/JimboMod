using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Jimbo.JimboCode.Powers;

public class HandSizePower : JimboPower, IMaxHandSizeModifier
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public int ModifyMaxHandSize(Player player, int currentMaxHandSize)
    {
        return currentMaxHandSize + Amount;
    }
}