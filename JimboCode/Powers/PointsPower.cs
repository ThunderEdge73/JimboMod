using MegaCrit.Sts2.Core.Entities.Powers;

namespace Jimbo.JimboCode.Powers;

public class PointsPower : JimboPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}