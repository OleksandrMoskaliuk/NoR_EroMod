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
                if (___key_item)
                {
                    if (___playerstatus.MP_Posion > 0)
                    {
                        ___playerstatus._USE_HPposion = 1;
                        ___playerstatus.Sp = ___playerstatus.AllMaxSP();
                        global::NoREroMod.Plugin.KnockDownPlayerTimeWindow = global::NoREroMod.Plugin.eliteGrabInvul.Value;
                        ___key_submit = true;
                        ___key_atk = true;
                        ___key_item = false;
                        ___downup = 1;
                        return;
                    }
                    if (___playerstatus.HP_Posion > 0)
                    {
                        ___playerstatus._USE_HPposion = 1;
                        ___playerstatus.Sp = ___playerstatus.AllMaxSP();
                        global::NoREroMod.Plugin.KnockDownPlayerTimeWindow = global::NoREroMod.Plugin.eliteGrabInvul.Value;
                        ___key_submit = true;
                        ___key_atk = true;
                        ___key_item = false;
                        ___downup = 1;
                        return;
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


        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "recovery_fun")]
        [global::HarmonyLib.HarmonyPrefix]
        private static bool SlowSPRecoveryWhileDown(global::playercon __instance, global::PlayerStatus ___playerstatus, bool ___Parry, ref float ___Tcount)
        {

            if (___playerstatus.Sp < ___playerstatus.AllMaxSP() && !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && !___Parry && global::UnityEngine.Time.timeScale != 0f)
            {
                float SpRegenWhenDowned = 0;
                if (__instance.guard)
                {
                    SpRegenWhenDowned = 7.5f;
                }
                else if (__instance.erodown == 0)
                {
                    SpRegenWhenDowned = 2f;
                }
                else
                {
                    // recovery will count each character parameter
                    float HpValuability = 8f;
                    float PleasureValuability = 4f;
                    float SpValuability = 4f;
                    float MpValuability = 4f;
                    float TotalDivider = SpValuability + HpValuability + PleasureValuability + MpValuability;
                    float MpRemain = ___playerstatus.Mp / ___playerstatus.AllMaxMP();
                    float PlRemain = 1f - (___playerstatus._BadstatusVal[0] / 100f * PleasureValuability);
                    float SpRemain = ___playerstatus.Sp / ___playerstatus.AllMaxSP() * SpValuability;
                    float HpRemain = ___playerstatus.Hp / ___playerstatus.AllMaxHP() * HpValuability;
                    float SumOfRemainedStats = MpRemain + PlRemain + SpRemain + HpRemain;
                    // Total result from 0..to..1
                    float TotalResult = 1f - (SumOfRemainedStats / TotalDivider);
                    // Immidiately get up at all stats 100%
                    float DynamicSpMin = global::UnityEngine.Mathf.Lerp(0.1f, global::NoREroMod.Plugin.pleasureSPRegenMin.Value, TotalResult);
                    SpRegenWhenDowned = global::UnityEngine.Mathf.Lerp(DynamicSpMin, global::NoREroMod.Plugin.pleasureSPRegenMax.Value, TotalResult);
                }
                ___playerstatus.Sp += ___playerstatus.AllMaxSP() / SpRegenWhenDowned * global::UnityEngine.Time.deltaTime;
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
            // Passive regeneration
            float Level = (float)___playerstatus.LV / 5f;
            float BirthCount = (float)___playerstatus.HaramiCount / 10f;
            float RapeCount = (float)___playerstatus.RapeCount / 100f;
            float TotalCumVolume = ___playerstatus.NakadashiValue / 1000f;
            float TotalRegenSource = BirthCount + RapeCount + TotalCumVolume + Level;
            float RegenerationStrength = (float)(0.2f * global::System.Math.Log(0.2 * TotalRegenSource + 1.0) *
                (1.0 * global::System.Math.Pow(TotalRegenSource, 0.5) + 2.71828182846f) + -1.9 * global::System.Math.Pow(1.0, TotalRegenSource) + 1.9);
            float RegenarationTime = RegenerationStrength * global::UnityEngine.Time.deltaTime * 0.1f;
            bool PassiveRegenCondition = !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && global::UnityEngine.Time.timeScale != 0f;

            if (___playerstatus.Sp < ___playerstatus.AllMaxSP() && PassiveRegenCondition)
            {
                ___playerstatus.Sp += ___playerstatus.AllMaxSP() * RegenarationTime;
            }
            if (___playerstatus.Sp < 0f)
            {
                ___playerstatus.Sp = 0f;
            }

            if (___playerstatus.Hp < ___playerstatus.AllMaxHP() && PassiveRegenCondition)
            {
                ___playerstatus.Hp += ___playerstatus.AllMaxHP() * RegenarationTime;
            }
            if (___playerstatus.Hp < 0f)
            {
                ___playerstatus.Hp = 0f;
            }

            if (___playerstatus.Mp < ___playerstatus.AllMaxMP() && PassiveRegenCondition)
            {
                //___playerstatus.Mp += ___playerstatus.AllMaxMP() * RegenarationTime * ___playerstatus.NakadashiValue * 100;
                //___playerstatus.Mp += ___playerstatus.InranCount * 100 * global::UnityEngine.Time.deltaTime;
                //___playerstatus.Mp += ___playerstatus.NakadashiValue / 10 * global::UnityEngine.Time.deltaTime;
            }
            if (___playerstatus.Mp < 0f)
            {
                ___playerstatus.Mp = 0f;
            }
            // return false -> turn off original method execution
            // return true -> turn on original method execution
            return false;
        }


        // Postfix version of  NoResetGuardOnParry
        // will execute after guard_fun original function
        // Perry will reset block time
        // On bad timing parry get perfect block instead damage 
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "guard_fun")]
        [global::HarmonyLib.HarmonyPostfix]
        public static void NoResetGuardOnParry(global::playercon __instance, global::PlayerStatus ___playerstatus,
             bool ___key_guard, bool ___Attacknow, int ___stepkind, bool ___nowdamage,
             bool ___magicnow, bool ___Itemuse, bool ___Death, ref bool ___Parry,
             ref float ___parrycount, ref float ___guradcount, float ___key_vertical)
        {
            // int stepkind = Traverse.Create(__instance).Field("stepkind").GetValue<int>();
            if (___Parry)
            {
                if (___key_guard && !___Attacknow && ___stepkind == 0 && !___nowdamage && !___magicnow && ___playerstatus._SOUSA && !___Itemuse && !___Death)
                {
                    if (__instance.m_Grounded)
                    {
                        __instance.guard = true;
                        ___parrycount += global::UnityEngine.Time.deltaTime / 2f;
                        if (___guradcount > 0f)
                        {
                            ___guradcount -= global::UnityEngine.Time.deltaTime / 2f;
                            if (___guradcount < 0f)
                            {
                                ___guradcount = 0f;
                            }
                        }
                        if (__instance.justguard < ___playerstatus._GuardCutTime + 0.2f)
                        {
                            __instance.justguard += global::UnityEngine.Time.deltaTime / 2f;
                        }
                        if (___key_vertical > 0.01f && ___parrycount > 0.01f && ___playerstatus.WeaponKind != 5)
                        {
                            ___playerstatus.PleasureParalysisActionPercentage();
                            ___Parry = true;
                            ___parrycount = 0f;

                        }
                    }
                    else if (!__instance.m_Grounded)
                    {
                        __instance.guard = false;
                        __instance.justguard = 0f;
                        ___parrycount = 0f;
                        ___guradcount = 0f;
                    }

                }
                else
                {
                    __instance.guard = false;
                    __instance.justguard = 0f;
                    ___guradcount = 0f;
                }
            }

        }

        // Token: 0x06000012 RID: 18 RVA: 0x00002073 File Offset: 0x00000273
        public PlayerConPatch()
        {
        }
    }
}
