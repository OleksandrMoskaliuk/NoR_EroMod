using System;
using HarmonyLib;

namespace NoREroMod
{
	// Token: 0x02000004 RID: 4
	internal class BuffPatch
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002069 File Offset: 0x00000269
		[global::HarmonyLib.HarmonyPatch(typeof(global::Buff), "PleasureParalysis")]
		[global::HarmonyLib.HarmonyPrefix]
		private static void NoPleasureRecovery(global::Buff __instance, bool ___Pleasureparalysis, global::PlayerStatus ___pl, ref float ___CountUp)
		{
			//___CountUp = 0f;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002110 File Offset: 0x00000310
		[global::HarmonyLib.HarmonyPatch(typeof(global::Buff), "ParalysisOrgasm")]
		[global::HarmonyLib.HarmonyPrefix]
		private static bool NoPleasureRecoveryOnOrgasm(global::Buff __instance, bool ___orgasm)
		{
			//if (___orgasm)
			//{
			//	__instance.Invoke("ParalysisOrgasmInvoke", 2f);
			//}
			return true;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002073 File Offset: 0x00000273
		public BuffPatch()
		{
		}
	}
}
