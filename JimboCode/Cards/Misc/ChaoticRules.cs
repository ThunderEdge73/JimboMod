using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace Jimbo.JimboCode.Cards.Misc;

public class ChaoticRules : JimboCard
{
    public ChaoticRules() : base(-1, CardType.Status,
        CardRarity.Status, TargetType.None)
    {
        WithCards(1);
        WithTip(StaticHoverTip.Transform);
    }
}