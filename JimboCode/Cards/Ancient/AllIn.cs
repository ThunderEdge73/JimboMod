using BaseLib.Utils;
using Jimbo.JimboCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Jimbo.JimboCode.Cards.Ancient;

public class AllIn : JimboCard
{
    public AllIn() : base(1, CardType.Attack,
        CardRarity.Ancient, TargetType.AllEnemies)
    {
        WithVar("ScorePercent", 10);
        WithCalculatedDamage(9, (model, _) => Math.Floor(
                MultChipsCmd.CalculatePointsEarned(model.Owner) * model.DynamicVars["ScorePercent"].BaseValue / 100),
            upgrade: 3);
        WithKeywords(JimboKeywords.Score, CardKeyword.Retain);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, vfx: "vfx/vfx_attack_slash").Execute(choiceContext);
    }
}