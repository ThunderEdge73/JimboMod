using Jimbo.JimboCode.Character;
using Jimbo.JimboCode.Misc;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Cards.Common;

public class GrosMichel : JimboCard
{
    public GrosMichel() : base(1, CardType.Skill,
        CardRarity.Common, TargetType.Self)
    {
        WithMult(10, 5);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await JimboUtils.PlusMult(this);
    }
}