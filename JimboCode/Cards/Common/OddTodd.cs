using BaseLib.Utils;
using Jimbo.JimboCode.Character;
using Jimbo.JimboCode.Misc;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Cards.Common;

public class OddTodd : JimboCard
{
    public OddTodd() : base(1, CardType.Attack,
        CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(9, 4);
        WithChips(5);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await JimboUtils.PlusChips(this);
        await CommonActions.CardAttack(this, play, vfx: "vfx/vfx_attack_slash").Execute(choiceContext);
    }
}