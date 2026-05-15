using BaseLib.Abstracts;
using Jimbo.JimboCode.Cards.Ancient;
using Jimbo.JimboCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Jimbo.JimboCode.Cards.Starter;

public class Bet() : JimboCard(1, CardType.Attack,
    CardRarity.Basic, TargetType.AnyEnemy), ITranscendenceCard
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(6),
        new ExtraDamageVar(1),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) =>
            Math.Floor(
                MultChipsCmd.CalculatePointsEarned(card.Owner) * card.DynamicVars["ScorePercent"].BaseValue / 100)),
        new("ScorePercent", 10)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [JimboKeywords.Score];

    public CardModel GetTranscendenceTransformedCard()
    {
        return ModelDb.Card<AllIn>();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.CalculatedDamage)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(3);
    }
}