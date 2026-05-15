using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Jimbo.JimboCode.Character;

public static class JimboKeywords
{
    [CustomEnum] [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Score;
    public static bool IsScore(this CardModel card)
    {
        return card.Keywords.Contains(Score);
    }
}