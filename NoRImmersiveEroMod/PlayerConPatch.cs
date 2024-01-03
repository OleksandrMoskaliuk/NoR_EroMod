using System;
using UnityEngine;

namespace NoRImmersiveEroMod
{
    // Token: 0x02000006 RID: 6
    internal class PlayerConPatch
    {
        // On rape
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "step_fun")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void PlayerMoveAndDashSpeed(global::playercon __instance, ref float ___MOVESPD)
        {

            // Move and Dash speed original
            ___MOVESPD = 5f;

            // Dash distance multiply it by 2
            // if 2.5 = original value 2.5 * 2 = 5, etc...
            __instance.movespeed = 3f;
        }

        // Escape from rape mechanics
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "Update")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void IncreaseStatusOnEro(global::playercon __instance, global::PlayerStatus ___playerstatus, ref bool ___key_submit, ref bool ___key_atk, ref int ___downup)
        {
            if (GameplayInfo.mEnemyDate != null && __instance.erodown != 0 && __instance.eroflag)
            {
                Plugin.gameplay.Update(___playerstatus);
                GameplayInfo ginfo = Plugin.gameplay;
                // Fight with enemy 
                float SpDamageMultiplier = GameplayInfo.mEnemyDate.MaxSp * 0.1f;
                float SpDamageToEnemy = ___playerstatus.Sp * UnityEngine.Time.deltaTime * SpDamageMultiplier;
                float SpDamageToPlayer = GameplayInfo.mEnemyDate.Sp * UnityEngine.Time.deltaTime * SpDamageMultiplier;
                bool EnemyHaveSt = ginfo.mEnemySp > SpDamageToEnemy;
                bool PlayerHaveSt = ___playerstatus.Sp > SpDamageToPlayer;
                bool CanEscape = ___playerstatus.Sp > ___playerstatus.AllMaxSP() * GameplayInfo.RapeEscapeThreshold;
                Plugin.gameplay.Update(___playerstatus);
                // Roll 10% current stats to get more or less depend on luck
                if (!CanEscape && ginfo.mIsPlayerAttack && ___playerstatus._BadstatusVal[0] < 99)
                {
                    // Roll dice while you down
                    ___playerstatus._BadstatusVal[0] += 10f / ginfo.mPlayerAdvantage;
                    ___playerstatus.Sp -= ___playerstatus.Sp * 0.1f * UnityEngine.Time.deltaTime;
                    ___playerstatus.Mp -= ___playerstatus.Mp * 0.1f * UnityEngine.Time.deltaTime;
                    ___playerstatus.Sp += ___playerstatus.AllMaxSP() * ginfo.mPlayerAdvantage * UnityEngine.Random.Range(0.001f, 0.5f) * UnityEngine.Time.deltaTime;
                }
                if (EnemyHaveSt && PlayerHaveSt)
                {
                    GameplayInfo.mEnemyDate.Sp -= SpDamageToEnemy;
                    ___playerstatus.Sp -= SpDamageToPlayer;
                }
                // If escape fail loose sp and hp while enemy recover
                if (___playerstatus._BadstatusVal[0] > 95 && ginfo.mIsEnemyClose)
                {
                    // On the end enemy will restore it heath and player will get restored amount as damage
                    float EnemyHpRecov = Math.Min((GameplayInfo.mEnemyDate.MaxHp - GameplayInfo.mEnemyDate.Hp), (___playerstatus.Hp * 0.01f));
                    float EnemySpRecov = Math.Min((GameplayInfo.mEnemyDate.MaxSp - GameplayInfo.mEnemyDate.Sp), (___playerstatus.Sp * 0.01f));
                    GameplayInfo.mEnemyDate.Hp += EnemyHpRecov * Time.deltaTime;
                    GameplayInfo.mEnemyDate.Sp += EnemySpRecov * Time.deltaTime;
                    ___playerstatus.Hp -= EnemyHpRecov * Time.deltaTime;
                    ___playerstatus.Sp -= EnemySpRecov * Time.deltaTime;
                    //___playerstatus._BadstatusVal[0] -= 10;
                }
                // Else just regenerate sp and escape when enemy will run out of sp
                Plugin.gameplay.Update(___playerstatus);
                //___playerstatus.Sp += ___playerstatus.AllMaxSP() * UnityEngine.Random.Range(ginfo.Buff(0.1f), ginfo.Buff(0.2f)) * ginfo.mPlayerAdvantage * UnityEngine.Time.deltaTime;
                GameplayInfo.mEnemyDate.Sp += GameplayInfo.mEnemyDate.MaxSp * UnityEngine.Random.Range(ginfo.BuffEnemy(0.1f), ginfo.BuffEnemy(0.2f)) * ginfo.mEnemyAdvantage * UnityEngine.Time.deltaTime;
                if (GameplayInfo.mEnemyDate.Sp >= GameplayInfo.mEnemyDate.MaxSp)
                {
                    GameplayInfo.mEnemyDate.Sp = GameplayInfo.mEnemyDate.MaxSp;
                }
                ___playerstatus.BadstatusValPlus(ginfo.BuffEnemy((float)Math.Pow(ginfo.mEnemyAdvantage,2f))  * global::UnityEngine.Time.deltaTime);
            }
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_nowdamage")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void DisableDownedRecoveryUnlessMaxSP(global::playercon __instance, global::PlayerStatus ___playerstatus, ref bool ___key_submit, ref bool ___key_atk, ref bool ___key_item, ref int ___downup)
        {
            bool flag = __instance.erodown != 0 && !__instance._easyESC && ___playerstatus._SOUSA && ___playerstatus.Sp < (___playerstatus.AllMaxSP() * 0.99);
            if (flag)
            {
                ___key_submit = false;
                ___key_atk = false;
                if (___key_item)
                {
                    if (___playerstatus.MP_Posion > 0)
                    {
                        ___playerstatus._USE_MPposion = 1;
                        ___playerstatus.Sp = ___playerstatus.AllMaxSP();
                        global::NoRImmersiveEroMod.Plugin.KnockDownPlayerTimeWindow = global::NoRImmersiveEroMod.Plugin.eliteGrabInvul.Value;
                        ___key_submit = true;
                        ___key_atk = true;
                        ___key_item = false;
                        ___downup = 1;
                        return;
                    }
                    else if (___playerstatus.HP_Posion > 0)
                    {
                        ___playerstatus._USE_HPposion = 1;
                        ___playerstatus.Sp = ___playerstatus.AllMaxSP();
                        global::NoRImmersiveEroMod.Plugin.KnockDownPlayerTimeWindow = global::NoRImmersiveEroMod.Plugin.eliteGrabInvul.Value;
                        ___key_submit = true;
                        ___key_atk = true;
                        ___key_item = false;
                        ___downup = 1;
                        return;
                    }
                }
            }
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage_Improvement")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void MoreDamage(ref float getatk, ref int kickbackkind, global::PlayerStatus ___playerstatus)
        {
            // Attack multiplier depend on ero stat
            bool flag = kickbackkind < 3 && global::UnityEngine.Random.value < ___playerstatus._BadstatusVal[0] / 100f;
            if (flag)
            {
                kickbackkind = 3;
            }
            getatk *= global::UnityEngine.Mathf.Lerp(global::NoRImmersiveEroMod.Plugin.pleasureEnemyAttackMin.Value, global::NoRImmersiveEroMod.Plugin.pleasureEnemyAttackMax.Value, ___playerstatus._BadstatusVal[0] / 100f);
        }

        // On enemy delayed strong hit 
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage_Improvement")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void OnPlayerDownZeroSP(int kickbackkind, global::playercon __instance, global::PlayerStatus ___playerstatus)
        {
            GameplayInfo ginfo = Plugin.gameplay;
            // Gain plesure on knock down
            if (ginfo.Update(___playerstatus) && kickbackkind >= 3 && __instance.erodown != 0)
            {
                // TODO: Calculate pleasure gain on strong hit when 
                ___playerstatus.BadstatusValPlus(global::NoRImmersiveEroMod.Plugin.pleasureGainOnDown.Value);
                ___playerstatus.Sp = ginfo.mPlayerLostStats * ginfo.mPlayerMaxSp * ginfo.mEnemyAdvantage;
            }
        }


        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "recovery_fun")]
        [global::HarmonyLib.HarmonyPrefix]
        private static bool SlowSPRecoveryWhileDown(global::playercon __instance, global::PlayerStatus ___playerstatus, bool ___Parry, ref float ___Tcount)
        {
            if (___playerstatus.Sp < ___playerstatus.AllMaxSP() && !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && !___Parry && global::UnityEngine.Time.timeScale != 0f)
            {
                Plugin.gameplay.Update(___playerstatus);
                GameplayInfo ginfo = Plugin.gameplay;
                float SpIncrement = ___playerstatus.AllMaxSP();
                if (__instance.state == playercon.GUARD)
                {
                    SpIncrement /= 16;
                }
                else if (__instance.erodown == 0)
                {
                    // Normal player state
                    SpIncrement /= 3;
                }
                else // Player knocked down
                {
                    SpIncrement /= 3;
                    // Check if enemy close
                    if (GameplayInfo.mEnemyDate.distance < 15f && GameplayInfo.mEnemyDate.distance > -15f)
                    {
                        SpIncrement = ___playerstatus.AllMaxSP();
                        ginfo.Update(___playerstatus);
                        // On max pleasure this will be equal 2
                        float PleasureEnemyBuff = UnityEngine.Mathf.Lerp(2f, 1f, ginfo.mPlayerPleasure);
                        // On max pleasure this will be equal 0.5
                        float PleasurePlayerBuff = UnityEngine.Mathf.Lerp(0.5f, 1f, ginfo.mPlayerPleasure);
                        float MinTimeToGetUp = 2f / (float)Math.Pow((ginfo.mPlayerAdvantage * PleasurePlayerBuff), ginfo.BuffPlayer(1.8f));
                        float MaxTimeToGetUp = 2f * (float)Math.Pow((ginfo.mEnemyAdvantage * PleasureEnemyBuff), ginfo.BuffEnemy(1.8f));
                        MinTimeToGetUp = Math.Max(3f, MinTimeToGetUp);
                        MaxTimeToGetUp = Math.Min(50f, MaxTimeToGetUp);
                        float PlayerCondition = 1 - (ginfo.mPlayerCurrentStats / ginfo.PlayerTotalStats);
                        SpIncrement /= global::UnityEngine.Mathf.Lerp(MinTimeToGetUp, MaxTimeToGetUp, PlayerCondition);
                    }
                }
                ___playerstatus.Sp += SpIncrement * global::UnityEngine.Time.deltaTime;
                if (___playerstatus.Sp < 0f)
                {
                    ___playerstatus.Sp = 0f;
                }
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
            float RegenerationStrength = GameplayInfo.RegenerationFromSource(TotalRegenSource);

            bool PassiveRegenCondition = !__instance.Attacknow && !__instance.Actstate
                && !__instance.stepfrag && !__instance.magicnow
                && global::UnityEngine.Time.timeScale != 0f;

            if (___playerstatus.Hp < ___playerstatus.AllMaxHP() && PassiveRegenCondition)
            {
                ___playerstatus.Hp += ___playerstatus.AllMaxHP() * RegenerationStrength * Time.deltaTime;
                if (___playerstatus.Hp < 0f)
                {
                    ___playerstatus.Hp = 0f;
                }
            }

            if (___playerstatus.Sp < ___playerstatus.AllMaxSP() && PassiveRegenCondition)
            {
                ___playerstatus.Sp += ___playerstatus.AllMaxSP() * RegenerationStrength * Time.deltaTime;
                if (___playerstatus.Sp < 0f)
                {
                    ___playerstatus.Sp = 0f;

                }
            }

            if (___playerstatus.Mp < ___playerstatus.AllMaxMP() && PassiveRegenCondition)
            {
                ___playerstatus.Mp += ___playerstatus.AllMaxMP() * RegenerationStrength * Time.deltaTime;

                if (___playerstatus.Mp < 0f)
                {
                    ___playerstatus.Mp = 0f;
                }
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
