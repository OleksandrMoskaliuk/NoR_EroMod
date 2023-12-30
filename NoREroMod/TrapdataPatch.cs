using System;
using HarmonyLib;

namespace NoREroMod
{
	// Token: 0x02000009 RID: 9
	internal class TrapdataPatch
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002C2C File Offset: 0x00000E2C
		[global::HarmonyLib.HarmonyPatch(typeof(global::Trapdata), "Nakadasi")]
		[global::HarmonyLib.HarmonyPostfix]
		private static void BirthOnCreampie(global::Trapdata __instance, global::PlayerStatus ___playerstatus)
		{
			bool flag = ___playerstatus._BadstatusVal[2] <= 0f && ___playerstatus._BadstatusVal[3] <= 0f;
			if (flag)
			{
				___playerstatus.CreampieVal_UI();
				__instance.com_player._BirthNumber = 1;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002073 File Offset: 0x00000273
		public TrapdataPatch()
		{
		}
	}
}
