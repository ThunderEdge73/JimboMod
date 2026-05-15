using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Jimbo.JimboCode.Cards.Misc;

public class SpeedyRules() : JimboCard(-1, CardType.Status,
    CardRarity.Status, TargetType.None)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Transform)];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("Cards", 1)];
}