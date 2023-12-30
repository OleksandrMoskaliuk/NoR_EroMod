using System;
using HarmonyLib;
using UnityEngine;

namespace NoREroMod
{
	// Token: 0x02000006 RID: 6
	internal class PlayerConPatch
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000023E0 File Offset: 0x000005E0
		[global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "Update")]
		[global::HarmonyLib.HarmonyPostfix]
		private static void IncreaseStatusOnEro(global::playercon __instance, global::PlayerStatus ___playerstatus)
		{
			bool flag = __instance.erodown != 0 && __instance.eroflag;
			if (flag)
			{
				___playerstatus.BadstatusValPlus(global::NoREroMod.Plugin.pleasureGainOnEro.Value * global::UnityEngine.Time.deltaTime);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000241C File Offset: 0x0000061C
		[global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_nowdamage")]
		[global::HarmonyLib.HarmonyPrefix]
		private static void DisableDownedRecoveryUnlessMaxSP(global::playercon __instance, global::PlayerStatus ___playerstatus, ref bool ___key_submit, ref bool ___key_atk, ref bool ___key_item, ref int ___downup)
		{
			bool flag = __instance.erodown != 0 && !__instance._easyESC && ___playerstatus._SOUSA && ___playerstatus.Sp < ___playerstatus.AllMaxSP();
			if (flag)
			{
				___key_submit = false;
				___key_atk = false;
				bool flag2 = ___key_item;
				if (flag2)
				{
					bool flag3 = ___playerstatus.HP_Posion > 0;
					if (flag3)
					{
						___playerstatus._USE_HPposion = 1;
						___playerstatus.Sp = ___playerstatus.AllMaxSP();
						global::NoREroMod.Plugin.eliteGrabInvulTimer = global::NoREroMod.Plugin.eliteGrabInvul.Value;
						___key_submit = true;
						___key_atk = true;
						___key_item = false;
						___downup = 1;
					}
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024A8 File Offset: 0x000006A8
		[global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage")]
		[global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage_Improvement")]
		[global::HarmonyLib.HarmonyPrefix]
		private static void MoreDamage(ref float getatk, ref int kickbackkind, global::PlayerStatus ___playerstatus)
		{
			bool flag = kickbackkind < 3 && global::UnityEngine.Random.value < ___playerstatus._BadstatusVal[0] / 100f;
			if (flag)
			{
				kickbackkind = 3;
			}
			getatk *= global::UnityEngine.Mathf.Lerp(global::NoREroMod.Plugin.pleasureEnemyAttackMin.Value, global::NoREroMod.Plugin.pleasureEnemyAttackMax.Value, ___playerstatus._BadstatusVal[0] / 100f);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002508 File Offset: 0x00000708
		[global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage")]
		[global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage_Improvement")]
		[global::HarmonyLib.HarmonyPostfix]
		private static void OnPlayerDownZeroSP(int kickbackkind, global::playercon __instance, global::PlayerStatus ___playerstatus)
		{
			bool flag = kickbackkind >= 3 && __instance.erodown != 0;
			if (flag)
			{
				___playerstatus.BadstatusValPlus(global::NoREroMod.Plugin.pleasureGainOnDown.Value);
				___playerstatus.Sp = 0f;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002548 File Offset: 0x00000748
		[global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "recovery_fun")]
		[global::HarmonyLib.HarmonyPrefix]
		private static bool SlowSPRecoveryWhileDown(global::playercon __instance, global::PlayerStatus ___playerstatus, bool ___Parry, ref float ___Tcount)
		{
			if (___playerstatus.Sp < ___playerstatus.AllMaxSP() && !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && !___Parry && global::UnityEngine.Time.timeScale != 0f)
			{
				float num;
				if (__instance.guard)
				{
					num = 7.5f;
				}
				else if (__instance.erodown == 0)
				{
					num = 2f;
				}
				else
				{
					float num2 = 8f;
					float num3 = 4f;
					float num4 = 4f;
					float num5 = 4f;
					float num6 = num4 + num2 + num3 + num5;
					float num7 = ___playerstatus.Mp / ___playerstatus.AllMaxMP();
					float num8 = ___playerstatus._BadstatusVal[0] / 100f * num3;
					float num9 = ___playerstatus.Sp / ___playerstatus.AllMaxSP() * num4;
					float num10 = ___playerstatus.Hp / ___playerstatus.AllMaxHP() * num2;
					float num11 = num8 + num10 + num9;
					num = global::UnityEngine.Mathf.Lerp(global::NoREroMod.Plugin.pleasureSPRegenMax.Value, global::NoREroMod.Plugin.pleasureSPRegenMin.Value, num11 / num6);
				}
				___playerstatus.Sp += ___playerstatus.AllMaxSP() / num * global::UnityEngine.Time.deltaTime;
			}
			if (___playerstatus.Sp < 0f)
			{
				___playerstatus.Sp = 0f;
			}
			if (__instance.tough < __instance.maxtough)
			{
				___Tcount += global::UnityEngine.Time.deltaTime;
				if (___Tcount > 1f)
				{
					__instance.tough = __instance.maxtough;
					___Tcount = 0f;
				}
			}
			double num12 = (double)___playerstatus.HaramiCount;
			int rapeCount = ___playerstatus.RapeCount;
			double num13 = (0.2 * global::System.Math.Log(0.2 * num12 + 1.0) * (1.0 * global::System.Math.Pow(num12, 0.5) + 2.71828182846) + -1.9 * global::System.Math.Pow(1.0, num12) + 1.9) / 10.0;
			num13 += (0.2 * global::System.Math.Log(0.2 * (double)rapeCount + 1.0) * (1.0 * global::System.Math.Pow((double)rapeCount, 0.5) + 2.71828182846) + -1.9 * global::System.Math.Pow(1.0, (double)rapeCount) + 1.9) / 100.0;
			if (___playerstatus.Sp < ___playerstatus.AllMaxSP() && !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && global::UnityEngine.Time.timeScale != 0f)
			{
				double num14 = 0.01 * (double)___playerstatus.AllMaxSP() * num13;
				___playerstatus.Sp += (float)num14;
			}
			if (___playerstatus.Sp < 0f)
			{
				___playerstatus.Sp = 0f;
			}
			if (___playerstatus.Hp < ___playerstatus.AllMaxHP() && !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && global::UnityEngine.Time.timeScale != 0f)
			{
				double num15 = 0.01 * (double)___playerstatus.AllMaxHP() * num13;
				___playerstatus.Hp += (float)num15 * global::UnityEngine.Time.deltaTime;
			}
			if (___playerstatus.Hp < 0f)
			{
				___playerstatus.Hp = 0f;
			}
			if (___playerstatus.Mp < ___playerstatus.AllMaxMP() && !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && global::UnityEngine.Time.timeScale != 0f)
			{
				double num16 = 0.01 * (double)___playerstatus.AllMaxMP() * num13;
				___playerstatus.Mp += (float)num16 * global::UnityEngine.Time.deltaTime;
			}
			if (___playerstatus.Mp < 0f)
			{
				___playerstatus.Mp = 0f;
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002073 File Offset: 0x00000273
		public PlayerConPatch()
		{
		}
	}
}
