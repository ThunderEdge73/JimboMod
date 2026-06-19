using BaseLib.Extensions;
using Jimbo.JimboCode.Character;
using Jimbo.JimboCode.Misc;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Jimbo.JimboCode.Cards.Starter;

public class Joke : JimboCard
{
    public Joke() : base(1, CardType.Skill,
        CardRarity.Basic, TargetType.Self)
    {
        WithMult(4, 2);
        WithPower<WeakPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await JimboUtils.PlusMult(this);
        ArgumentNullException.ThrowIfNull(CombatState);
        var targetedEnemy = Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies);
        if (targetedEnemy != null)
            await PowerCmd.Apply<WeakPower>(choiceContext, targetedEnemy,
                DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
    }
}