using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Jimbo.JimboCode.Character;

public partial class MultChipsUi : Control
{
    private const decimal ESwitchPoint = 100000000000;

    public static AddedNode<NCreature, Control> MultChipsNode = new("res://Jimbo/ui/mult_chips.tscn", (_, ui) =>
    {
        ui.Position = new Vector2(0, -450);
        ui.Visible = false;
    });

    public static void UpdateMultText(Player player, decimal mult)
    {
        var creatureNode = player.Creature.GetCreatureNode();
        if (creatureNode == null) return;
        var uiNode = MultChipsNode.Get(creatureNode);
        if (uiNode == null) return;
        uiNode.Visible = true;
        var multLabel = uiNode.GetNode<RichTextLabel>("MultChips/MultChipsRow/MultBox/MultNum");
        multLabel.Text = "[wave amp=20 freq=4]" + NumberFormat(mult);
        multLabel.PivotOffset = multLabel.Size / 2f;
        var tween = uiNode.CreateTween().SetParallel().SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Linear);
        var rng = new RandomNumberGenerator();
        tween.TweenProperty(multLabel, "rotation", 0, 0.2f).From(rng.Randf() >= 0.5 ? 0.1f : -0.1f);
        tween.TweenProperty(multLabel, "scale", Vector2.One, 0.2f).From(new Vector2(1.1f, 1.1f));
    }

    public static void UpdateChipsText(Player player, decimal chips)
    {
        var creatureNode = player.Creature.GetCreatureNode();
        if (creatureNode == null) return;
        var uiNode = MultChipsNode.Get(creatureNode);
        if (uiNode == null) return;
        uiNode.Visible = true;
        var chipsLabel = uiNode.GetNode<RichTextLabel>("MultChips/MultChipsRow/ChipsBox/ChipsNum");
        chipsLabel.Text = "[wave amp=20 freq=4]" + NumberFormat(chips);
        chipsLabel.PivotOffset = chipsLabel.Size / 2f;
        var tween = uiNode.CreateTween().SetParallel().SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Linear);
        var rng = new RandomNumberGenerator();
        chipsLabel.Rotation = rng.Randf() >= 0.5 ? 0.1f : -0.1f;
        Random.Shared.NextDouble();
        chipsLabel.Scale = new Vector2(1.1f, 1.1f);
        tween.TweenProperty(chipsLabel, "rotation", 0, 0.2f);
        tween.TweenProperty(chipsLabel, "scale", Vector2.One, 0.2f);
    }

    private static string NumberFormat(decimal num)
    {
        var returned = num.ToString("N0");
        if (num < ESwitchPoint) return returned;
        var exp = returned.Length;
        var mantissa = decimal.Parse(returned.Substring(0, 4)) / 1000;
        return mantissa.ToString("N") + "e" + exp;
    }
}