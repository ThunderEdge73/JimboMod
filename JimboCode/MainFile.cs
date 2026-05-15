using BaseLib.Audio;
using Godot;
using HarmonyLib;
using Jimbo.JimboCode.Extensions;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace Jimbo.JimboCode;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "Jimbo"; //Used for resource filepath
    public const string ResPath = $"res://{ModId}";

    public static readonly IDictionary<string, ModSound> JimboSounds = new Dictionary<string, ModSound>();

    private static readonly IEnumerable<string> JimboSoundPaths =
    [
        "mod_score.ogg",
        "plus_chips.ogg",
        "plus_mult.ogg",
        "x_mult.ogg"
    ];

    public static Logger Logger { get; } =
        new(ModId, LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);

        foreach (var path in JimboSoundPaths)
        {
            var fixedPath = path.SfxPath();
            JimboSounds.Add(path, new ModSound(fixedPath));
        }

        harmony.PatchAll();
    }
}