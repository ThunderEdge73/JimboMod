using BaseLib.Abstracts;
using BaseLib.Extensions;
using Jimbo.JimboCode.Extensions;
using Godot;

namespace Jimbo.JimboCode.Powers;

public abstract class JimboPower : CustomPowerModel
{
    //Loads from Jimbo/images/powers/your_power.png
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}