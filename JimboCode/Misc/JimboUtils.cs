using MegaCrit.Sts2.Core.Models;

namespace Jimbo.JimboCode.Misc;

public static class JimboUtils
{
    public static Task PlusChips(CardModel card)
    {
        MultChipsCmd.AddChips(card.Owner, card.DynamicVars["Chips"].BaseValue);
        return Task.CompletedTask;
    }
    public static Task PlusMult(CardModel card)
    {
        MultChipsCmd.AddMult(card.Owner, card.DynamicVars["Mult"].BaseValue);
        return Task.CompletedTask;
    }
    public static Task XMult(CardModel card)
    {
        MultChipsCmd.MultiplyMult(card.Owner, card.DynamicVars["XMult"].BaseValue);
        return Task.CompletedTask;
    }
}