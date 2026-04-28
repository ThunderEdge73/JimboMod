using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Jimbo.JimboCode.Character;
using Jimbo.JimboCode.Extensions;
using Godot;

namespace Jimbo.JimboCode.Relics;

[Pool(typeof(JimboRelicPool))]
public abstract class JimboRelic : CustomRelicModel
{
    public override string PackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();

    protected override string PackedIconOutlinePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();

    protected override string BigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
}