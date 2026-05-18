using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using Jimbo.JimboCode.Cards.Starter;
using Jimbo.JimboCode.Extensions;
using Jimbo.JimboCode.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace Jimbo.JimboCode.Character;

public class Jimbo : PlaceholderCharacterModel
{
    public const string CharacterId = "Jimbo";

    public static readonly Color Color = new("3c4368");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeJimbo>(),
        ModelDb.Card<StrikeJimbo>(),
        ModelDb.Card<StrikeJimbo>(),
        ModelDb.Card<StrikeJimbo>(),
        ModelDb.Card<DefendJimbo>(),
        ModelDb.Card<DefendJimbo>(),
        ModelDb.Card<DefendJimbo>(),
        ModelDb.Card<DefendJimbo>(),
        ModelDb.Card<Bet>(),
        ModelDb.Card<Joke>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<Pluto>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<JimboCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<JimboRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<JimboPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomEnergyCounterPath => "jimbo_energy_counter.tscn".ScenePath();
    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();

    public override string CustomVisualPath => "jimbo_place_holder.tscn".ScenePath();
}