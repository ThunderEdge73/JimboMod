using BaseLib.Utils;
using Godot;
using HarmonyLib;
using Jimbo.JimboCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Jimbo.JimboCode.Misc;

public partial class MultChipsUi : Control
{
    private const decimal ESwitchPoint = 10_000_000;

    public static AddedNode<NCreature, Control> MultChipsNode = new("mult_chips.tscn".ScenePath(), (creature, ui) =>
    {
        var capacity = creature.Entity.Player?.PlayerCombatState?.OrbQueue.Capacity ?? 0;
        var y = GetYOffset(capacity);
        ui.Position = new Vector2(0, y);
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
        var tween = uiNode.CreateTween().SetParallel().SetEase(Tween.EaseType.Out);
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
        var tween = uiNode.CreateTween().SetParallel().SetEase(Tween.EaseType.Out);
        var rng = new RandomNumberGenerator();
        tween.TweenProperty(chipsLabel, "rotation", 0, 0.2f).From(rng.Randf() >= 0.5 ? 0.1f : -0.1f);
        tween.TweenProperty(chipsLabel, "scale", Vector2.One, 0.2f).From(new Vector2(1.1f, 1.1f));
    }

    private static string NumberFormat(decimal num)
    {
        if (num >= ESwitchPoint)
        {
            var exp = (int)Math.Log10((double)num);
            var mantissa = (double)num / Math.Pow(10, exp);
            var mantissaString = exp switch
            {
                >= 100 => mantissa.ToString("N1"),
                >= 10 => mantissa.ToString("N2"),
                _ => mantissa.ToString("N3")
            };
            while (mantissaString.EndsWith('0')) mantissaString = mantissaString[..^1];
            if (mantissaString.EndsWith('.')) mantissaString += "0";
            return mantissaString + "e" + exp;
        }

        string formatted;
        if (Math.Floor(num) != num && num < 100)
        {
            formatted = num.ToString(num >= 10 ? "N1" : "N2");
            if (num < 0.01M) return num.ToString("N");
            while (formatted.EndsWith('0')) formatted = formatted[..^1];
            if (formatted.EndsWith('.')) formatted = formatted[..^1];
        }
        else
        {
            formatted = num.ToString("N0");
        }

        return formatted;
    }

    public static int GetYOffset(int orbCapacity)
    {
        return orbCapacity >= 3 ? -575 - Math.Max(orbCapacity - 6, 0) * 8 : -450;
    }
}

[HarmonyPatch(typeof(OrbCmd), "AddSlots")]
internal static class OrbLimitIncreasedPatch
{
    [HarmonyPostfix]
    private static void AdjustMultChipsUiHeight(Player player, int amount)
    {
        var creatureNode = player.Creature.GetCreatureNode();
        if (creatureNode == null) return;
        var uiNode = MultChipsUi.MultChipsNode.Get(creatureNode);
        if (uiNode == null) return;
        var tween = uiNode.CreateTween().SetEase(Tween.EaseType.Out);
        var capacity = player.PlayerCombatState?.OrbQueue.Capacity ?? 0;
        var y = MultChipsUi.GetYOffset(capacity);
        tween.TweenProperty(uiNode, "position", new Vector2(0, y), 0.25f).SetDelay(0.1);
    }
}

[HarmonyPatch(typeof(OrbCmd), "RemoveSlots")]
internal static class OrbLimitDecreasedPatch
{
    [HarmonyPostfix]
    private static void AdjustMultChipsUiHeight(Player player, int amount)
    {
        var creatureNode = player.Creature.GetCreatureNode();
        if (creatureNode == null) return;
        var uiNode = MultChipsUi.MultChipsNode.Get(creatureNode);
        if (uiNode == null) return;
        var tween = uiNode.CreateTween().SetEase(Tween.EaseType.Out);
        var capacity = player.PlayerCombatState?.OrbQueue.Capacity ?? 0;
        var y = MultChipsUi.GetYOffset(capacity);
        tween.TweenProperty(uiNode, "position", new Vector2(0, y), 0.25f).SetDelay(0.1);
    }
}