using BaseLib.Abstracts;
using BaseLib.Utils;
using Jimbo.JimboCode.Cards.Ancient;
using Jimbo.JimboCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Jimbo.JimboCode.Cards.Starter;

public class Bet : JimboCard, ITranscendenceCard
{
    public Bet() : base(1, CardType.Attack,
        CardRarity.Basic, TargetType.AnyEnemy)
    {
        WithVar("ScorePercent", 10);
        WithCalculatedDamage(6, (model, _) => Math.Floor(
                MultChipsCmd.CalculatePointsEarned(model.Owner) * model.DynamicVars["ScorePercent"].BaseValue / 100),
            upgrade: 3);
        WithKeyword(JimboKeywords.Score);
    }

    public CardModel GetTranscendenceTransformedCard()
    {
        return ModelDb.Card<AllIn>();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, vfx: "vfx/vfx_attack_slash").Execute(choiceContext);
    }
}