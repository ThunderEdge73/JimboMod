using BaseLib.Extensions;
using Jimbo.JimboCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Jimbo.JimboCode.Cards.Starter;

public class Joke() : JimboCard(1, CardType.Skill,
    CardRarity.Basic, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("Mult", 4), new PowerVar<WeakPower>(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        MultChipsCmd.AddMult(play.Card.Owner, DynamicVars["Mult"].IntValue);
        var targetedEnemy = CombatState != null
            ? Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies)
            : null;
        if (targetedEnemy != null)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, targetedEnemy,
                DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Mult"].UpgradeValueBy(2);
    }
}