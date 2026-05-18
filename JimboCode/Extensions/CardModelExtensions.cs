using Jimbo.JimboCode.Cards.Misc;
using Jimbo.JimboCode.Character;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Jimbo.JimboCode.Extensions;

public static class CardModelExtensions
{
    public static bool IsType(this CardModel card, CardType type, CardModel? checkingCard = null)
    {
        return card.Type == type || (checkingCard != null && checkingCard.IsStrategic() && card is Tag);
    }
}