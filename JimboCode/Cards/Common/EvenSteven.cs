using BaseLib.Utils;
using Jimbo.JimboCode.Character;
using Jimbo.JimboCode.Misc;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Cards.Common;

public class EvenSteven : JimboCard
{
    public EvenSteven() : base(1, CardType.Attack,
        CardRarity.Common, TargetType.AllEnemies)
    {
        WithDamage(8, 4);
        WithMult(2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await JimboUtils.PlusMult(this);
        await CommonActions.CardAttack(this, play, vfx: "vfx/vfx_attack_slash").Execute(choiceContext);
    }
}