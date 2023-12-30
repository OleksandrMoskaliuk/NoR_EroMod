using System;
using HarmonyLib;
using UnityEngine;

namespace NoREroMod
{
	// Token: 0x02000007 RID: 7
	internal class PlayerStatusPatch
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000020A8 File Offset: 0x000002A8
		[global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_atk_speed", global::HarmonyLib.MethodType.Getter)]
		[global::HarmonyLib.HarmonyPostfix]
		private static void IncreaseAttackSpeed(global::PlayerStatus __instance, ref float __result)
		{
			__result *= global::UnityEngine.Mathf.Lerp(global::NoREroMod.Plugin.pleasureAttackSpeedMin.Value, global::NoREroMod.Plugin.pleasureAttackSpeedMax.Value, __instance._BadstatusVal[0] / 100f);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020D7 File Offset: 0x000002D7
		[global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_ATK", global::HarmonyLib.MethodType.Getter)]
		[global::HarmonyLib.HarmonyPostfix]
		private static void DecreaseAttackDamage(global::PlayerStatus __instance, ref float __result)
		{
			__result *= global::UnityEngine.Mathf.Lerp(global::NoREroMod.Plugin.pleasurePlayerAttackMin.Value, global::NoREroMod.Plugin.pleasurePlayerAttackMax.Value, __instance._BadstatusVal[0] / 100f);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002073 File Offset: 0x00000273
		public PlayerStatusPatch()
		{
		}
	}
}
