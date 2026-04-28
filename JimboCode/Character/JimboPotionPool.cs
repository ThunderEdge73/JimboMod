using BaseLib.Abstracts;
using Jimbo.JimboCode.Extensions;
using Godot;

namespace Jimbo.JimboCode.Character;

public class JimboPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => Jimbo.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}