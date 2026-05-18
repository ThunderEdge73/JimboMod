using BaseLib.Utils;
using Jimbo.JimboCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Cards.Common;

public class OddTodd : JimboCard
{
    public OddTodd() : base(1, CardType.Attack,
        CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(9, 4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, vfx: "vfx/vfx_attack_slash").Execute(choiceContext);
    }
}