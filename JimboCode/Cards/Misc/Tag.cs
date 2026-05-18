using Jimbo.JimboCode.Cards;
using Jimbo.JimboCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Jimbo.JimboCode.Cards.Misc;

public class Tag : JimboCard
{
    public Tag(): base(0, CardType.Skill,
        CardRarity.Token, TargetType.Self)
    {
        WithVar("Chips", 5, 5);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await JimboUtils.PlusChips(this);
    }
}