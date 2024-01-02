using System;
using HarmonyLib;
using UnityEngine;

namespace NoRImmersiveEroMod
{
	internal class PlayerStatusPatch
	{

		[global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_atk_speed", global::HarmonyLib.MethodType.Getter)]
		[global::HarmonyLib.HarmonyPostfix]
		private static void IncreaseAttackSpeed(global::PlayerStatus __instance, ref float __result)
		{
			__result *= global::UnityEngine.Mathf.Lerp(global::NoRImmersiveEroMod.Plugin.pleasureAttackSpeedMin.Value, global::NoRImmersiveEroMod.Plugin.pleasureAttackSpeedMax.Value, __instance._BadstatusVal[0] / 100f);
		}

		[global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_ATK", global::HarmonyLib.MethodType.Getter)]
		[global::HarmonyLib.HarmonyPostfix]
		private static void DecreaseAttackDamage(global::PlayerStatus __instance, ref float __result)
		{
			__result *= global::UnityEngine.Mathf.Lerp(global::NoRImmersiveEroMod.Plugin.pleasurePlayerAttackMin.Value, global::NoRImmersiveEroMod.Plugin.pleasurePlayerAttackMax.Value, __instance._BadstatusVal[0] / 100f);
		}

		public PlayerStatusPatch()
		{
		}
	}
}
