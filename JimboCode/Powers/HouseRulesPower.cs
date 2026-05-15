using Jimbo.JimboCode.Cards.Misc;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Jimbo.JimboCode.Powers;

public class HouseRulesPower : JimboPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.Static(StaticHoverTip.Transform)];

    private static IReadOnlyList<CardModel> TransformOptions =>
    [
        ModelDb.Card<SpeedyRules>(),
        ModelDb.Card<EnduranceRules>(),
        ModelDb.Card<ChaoticRules>(),
    ];

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player) return;
        var selectedOption =
            await CardSelectCmd.FromChooseACardScreen(new BlockingPlayerChoiceContext(),
                TransformOptions.Select(model =>
                {
                    var cardModel = CombatState.CreateCard(model, player);
                    cardModel.DynamicVars["Cards"].BaseValue = Amount;
                    return cardModel;
                }).ToList(), player, true);
        if (selectedOption == null) return;
        var selectedCards = await CardSelectCmd.FromHand(choiceContext, player,
            new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, Amount), null, this);
        var filterForType = selectedOption switch
        {
            ChaoticRules _ => CardType.Power,
            SpeedyRules _ => CardType.Attack,
            EnduranceRules _ => CardType.Skill,
            _ => CardType.None
        };
        foreach (var card in selectedCards)
        {
            var transformTarget = CardFactory.GetDistinctForCombat(player,
                    player.Character.CardPool.GetUnlockedCards(player.UnlockState,
                        player.RunState.CardMultiplayerConstraint).Where(model => model.Type == filterForType), 1,
                    player.RunState.Rng.CombatCardGeneration)
                .First();
            await CardCmd.Transform(card, transformTarget);
        }
    }
}