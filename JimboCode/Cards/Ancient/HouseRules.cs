using BaseLib.Extensions;
using Jimbo.JimboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Jimbo.JimboCode.Cards.Ancient;

public class HouseRules : JimboCard
{
    public HouseRules() : base(1, CardType.Power,
        CardRarity.Ancient, TargetType.Self)
    {
        WithVar(new PowerVar<HouseRulesPower>(1));
        WithTip(StaticHoverTip.Transform);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<HouseRulesPower>(choiceContext, Owner.Creature,
            DynamicVars.Power<HouseRulesPower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}