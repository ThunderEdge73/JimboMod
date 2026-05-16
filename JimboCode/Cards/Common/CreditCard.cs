using BaseLib.Extensions;
using BaseLib.Utils;
using Jimbo.JimboCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Cards.Common;

public class CreditCard : JimboCard
{
    public CreditCard() : base(1, CardType.Skill,
        CardRarity.Common, TargetType.Self)
    {
        WithBlock(8, 4);
        WithPower<LiabilityPower>(8);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<LiabilityPower>(choiceContext, Owner.Creature,
            DynamicVars.Power<LiabilityPower>().BaseValue, Owner.Creature, this);
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4);
    }
}