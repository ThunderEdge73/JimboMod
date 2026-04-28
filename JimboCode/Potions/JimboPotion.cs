using BaseLib.Abstracts;
using BaseLib.Utils;
using Jimbo.JimboCode.Character;

namespace Jimbo.JimboCode.Potions;

[Pool(typeof(JimboPotionPool))]
public abstract class JimboPotion : CustomPotionModel;