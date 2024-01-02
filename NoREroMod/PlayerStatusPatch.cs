using System;
using HarmonyLib;
using UnityEngine;

namespace NoREroMod
{
	internal class PlayerStatusPatch
	{

		[global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_atk_speed", global::HarmonyLib.MethodType.Getter)]
		[global::HarmonyLib.HarmonyPostfix]
		private static void IncreaseAttackSpeed(global::PlayerStatus __instance, ref float __result)
		{
			__result *= global::UnityEngine.Mathf.Lerp(global::NoREroMod.Plugin.pleasureAttackSpeedMin.Value, global::NoREroMod.Plugin.pleasureAttackSpeedMax.Value, __instance._BadstatusVal[0] / 100f);
		}

		[global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_ATK", global::HarmonyLib.MethodType.Getter)]
		[global::HarmonyLib.HarmonyPostfix]
		private static void DecreaseAttackDamage(global::PlayerStatus __instance, ref float __result)
		{
			__result *= global::UnityEngine.Mathf.Lerp(global::NoREroMod.Plugin.pleasurePlayerAttackMin.Value, global::NoREroMod.Plugin.pleasurePlayerAttackMax.Value, __instance._BadstatusVal[0] / 100f);
		}

		public PlayerStatusPatch()
		{
		}
	}
}
