using BaseLib.Patches.Content;
using BaseLib.Patches.Features;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Jimbo.JimboCode.Character;

// public static class JimboCustomTargetType
// {
//     [CustomEnum] public static TargetType AllEvenHp;
//     [CustomEnum] public static TargetType AnyOddHp;
// }
//
// [HarmonyPatch(typeof (ModelDb), "Init")]
// internal static class ModelDbTargetTypeInitPatch
// {
//     [HarmonyPostfix]
//     private static void RegisterTargetTypes()
//     {
//         CustomTargetType.RegisterMultiTargetType(JimboCustomTargetType.AllEvenHp, 
//             target => target is { IsAlive: true, IsEnemy: true } && target.CurrentHp % 2 == 0);
//         CustomTargetType.RegisterSingleTargetType(JimboCustomTargetType.AnyOddHp, 
//             target => target is { IsAlive: true, IsEnemy: true } && target.CurrentHp % 2 == 1);
//     }
// }