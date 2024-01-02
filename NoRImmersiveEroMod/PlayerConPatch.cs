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

        // On rape
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "Update")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void IncreaseStatusOnEro(global::playercon __instance, global::PlayerStatus ___playerstatus, ref bool ___key_submit, ref bool ___key_atk, ref int ___downup)
        {

            if (Plugin.EneymyData != null && __instance.erodown != 0 && __instance.eroflag)
            {
                // Gain pleasure from each enemy jub
                ___playerstatus.BadstatusValPlus(5f * global::UnityEngine.Time.deltaTime);
                // Vars
                EnemyDate enemy = Plugin.EneymyData;
                global::Rewired.Player player = global::Rewired.ReInput.players.GetPlayer(__instance.playerId);
                bool PlayerOnGuard = player.GetButton("Guard");
                bool IsPlayerAttack = player.GetButtonDown("Attack");
                bool IsSubmitKeyPressed = player.GetButtonDown("Submit");

                // Valuability coeficients for each stat
                float HpValuability = 8f;
                float SpValuability = 6f;
                float MpValuability = 2.5f;
                float PleasureValuability = 4f;
                float SpRegenWhenDowned = 2;

                // Calculate closest  enemy stats
                float EnemyTotalMaxStats = (Plugin.EneymyData.MaxHp * HpValuability) + (Plugin.EneymyData.MaxSp * SpValuability) + (Plugin.EneymyData.MaxMp * MpValuability);
                float EnemyCurrentStats = (Plugin.EneymyData.Hp * HpValuability) + (Plugin.EneymyData.Sp * SpValuability) + (Plugin.EneymyData.Mp * MpValuability);
                float EnemyTotalStats = EnemyTotalMaxStats + EnemyCurrentStats;
                bool EmenyWeakState = EnemyCurrentStats < EnemyTotalMaxStats / 4f;

                // Calculaet player stats to compare
                float PlayerTotalMaxStats = (___playerstatus.AllMaxHP() * HpValuability) + (___playerstatus.AllMaxSP() * SpValuability) + (___playerstatus.AllMaxMP() * MpValuability);
                float PlayerCurrentStats = (___playerstatus.Hp * HpValuability) + (___playerstatus.Sp * SpValuability) + (___playerstatus.Mp * MpValuability);
                float PlayerPleasure = ___playerstatus._BadstatusVal[0] * PleasureValuability;
                float PlayerTotalStats = PlayerTotalMaxStats + PlayerCurrentStats - PlayerPleasure;
                bool PlayerWeakState = PlayerCurrentStats < PlayerTotalMaxStats / 4f;

                // Compare enemy and palyer
                // Smaller coeficient will give stronger sp regeneration
                float StatsCoefCompared = (PlayerTotalStats / EnemyTotalStats);
                SpRegenWhenDowned *= StatsCoefCompared;

                // Who is stronger
                bool EnemyStronger = EnemyTotalStats * 0.7f > PlayerTotalStats;
                bool PlayerStronger = PlayerTotalStats * 0.7f > EnemyTotalStats;

                // Bonuses and debufs
                bool buf_01 = PlayerStronger && EmenyWeakState;
                bool buf_02 = PlayerStronger;
                bool buf_03 = EmenyWeakState;

                bool debuf_01 = EnemyStronger && PlayerWeakState;
                bool debuf_02 = EnemyStronger;
                bool debuf_03 = PlayerWeakState;

                if (buf_01)
                {
                    SpRegenWhenDowned *= 1.4f;
                }
                if (buf_02)
                {
                    SpRegenWhenDowned *= 1.4f;
                }
                if (buf_03)
                {
                    SpRegenWhenDowned *= 1.4f;
                }
                if (debuf_01)
                {
                    SpRegenWhenDowned /= 1.4f;
                }
                if (debuf_02)
                {
                    SpRegenWhenDowned /= 1.4f;
                }
                if (debuf_03)
                {
                    SpRegenWhenDowned /= 1.4f;
                }
                // Fight with enemy 
                float SpDamageMultiplier = enemy.MaxSp * 0.1f;
                float SpDamageToEnemy = ___playerstatus.Sp * UnityEngine.Time.deltaTime * SpDamageMultiplier;
                float SpDamageToPlayer = enemy.Sp * UnityEngine.Time.deltaTime * SpDamageMultiplier;
                bool EnemyHaveSt = enemy.Sp > SpDamageToEnemy;
                bool PlayerHaveSt = ___playerstatus.Sp > SpDamageToPlayer;
                bool CanEscape = ___playerstatus.Sp > ___playerstatus.AllMaxSP() * 0.99;
                if (!CanEscape && IsPlayerAttack && ___playerstatus._BadstatusVal[0] < 99)
                {
                    ___playerstatus.Sp += ___playerstatus.AllMaxSP() * 0.1f;
                    ___playerstatus._BadstatusVal[0] += 10;
                }
                if (EnemyHaveSt && PlayerHaveSt)
                {
                    enemy.Sp -= SpDamageToEnemy;
                    ___playerstatus.Sp -= SpDamageToPlayer;
                }
                // Else just regenerate sp and escape when enemy will run out of sp
                ___playerstatus.Sp += SpRegenWhenDowned * UnityEngine.Time.deltaTime;
            }
        }

        // Token: 0x0600000E RID: 14 RVA: 0x0000241C File Offset: 0x0000061C
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_nowdamage")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void DisableDownedRecoveryUnlessMaxSP(global::playercon __instance, global::PlayerStatus ___playerstatus, ref bool ___key_submit, ref bool ___key_atk, ref bool ___key_item, ref int ___downup)
        {
            Plugin.LoggerMessage02 = "DisableDownedRecoveryUnlessMaxSP";
            Plugin.LoggerMessage02 = "DisableDownedRecoveryUnlessMaxSP ";
            bool flag = __instance.erodown != 0 && !__instance._easyESC && ___playerstatus._SOUSA && ___playerstatus.Sp < (___playerstatus.AllMaxSP() * 0.99);
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

        // Token: 0x0600000F RID: 15 RVA: 0x000024A8 File Offset: 0x000006A8
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

        // Token: 0x06000010 RID: 16 RVA: 0x00002508 File Offset: 0x00000708
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage_Improvement")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void OnPlayerDownZeroSP(int kickbackkind, global::playercon __instance, global::PlayerStatus ___playerstatus)
        {
            // Gain plesure on knock down
            bool flag = kickbackkind >= 3 && __instance.erodown != 0;
            if (flag)
            {
                ___playerstatus.BadstatusValPlus(global::NoRImmersiveEroMod.Plugin.pleasureGainOnDown.Value);
                ___playerstatus.Sp = 0f;
            }
        }


        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "recovery_fun")]
        [global::HarmonyLib.HarmonyPrefix]
        private static bool SlowSPRecoveryWhileDown(global::playercon __instance, global::PlayerStatus ___playerstatus, bool ___Parry, ref float ___Tcount)
        {
            Plugin.LoggerMessage01 = " ";
            if (___playerstatus.Sp < ___playerstatus.AllMaxSP() && !__instance.Attacknow && !__instance.Actstate && !__instance.stepfrag && !__instance.magicnow && !___Parry && global::UnityEngine.Time.timeScale != 0f)
            {
                float SpRegenWhenDowned = ___playerstatus.AllMaxSP() / 2;
                // Player idle
                if (__instance.guard)
                {
                    SpRegenWhenDowned = 7.5f;
                }
                // Player on guard
                else if (__instance.erodown == 0)
                {
                    SpRegenWhenDowned = 2f;
                }
                // Player knocked down
                else
                {
                    // Valuability coeficients for each stat
                    float HpValuability = 8f;
                    float SpValuability = 6f;
                    float MpValuability = 2.5f;
                    float PleasureValuability = 4f;

                    // Calculate erodown recovery 
                    float TotalDivider = SpValuability + HpValuability + PleasureValuability + MpValuability;
                    float MpRemain = ___playerstatus.Mp / ___playerstatus.AllMaxMP();
                    float PlRemain = 1f - (___playerstatus._BadstatusVal[0] / 100f * PleasureValuability);
                    float SpRemain = ___playerstatus.Sp / ___playerstatus.AllMaxSP() * SpValuability;
                    float HpRemain = ___playerstatus.Hp / ___playerstatus.AllMaxHP() * HpValuability;
                    float SumOfRemainedStats = MpRemain + PlRemain + SpRemain + HpRemain;
                    // Total result from 0..to..1
                    float TotalResult = 1f - (SumOfRemainedStats / TotalDivider);
                    Plugin.LoggerMessage01 = "TotalResult: " + TotalResult.ToString();
                    // Immidiately get up at all stats 100%
                    float DynamicSpMin = global::UnityEngine.Mathf.Lerp(0.1f, global::NoRImmersiveEroMod.Plugin.pleasureSPRegenMin.Value, TotalResult);
                    SpRegenWhenDowned = global::UnityEngine.Mathf.Lerp(DynamicSpMin, global::NoRImmersiveEroMod.Plugin.pleasureSPRegenMax.Value, TotalResult);

                    if (Plugin.EneymyData != null)
                    {
                        // Calculate closest  enemy stats
                        float EnemyTotalMaxStats = (Plugin.EneymyData.MaxHp * HpValuability) + (Plugin.EneymyData.MaxSp * SpValuability) + (Plugin.EneymyData.MaxMp * MpValuability);
                        float EnemyCurrentStats = (Plugin.EneymyData.Hp * HpValuability) + (Plugin.EneymyData.Sp * SpValuability) + (Plugin.EneymyData.Mp * MpValuability);
                        float EnemyTotalStats = EnemyTotalMaxStats + EnemyCurrentStats;
                        bool EmenyWeakState = EnemyCurrentStats < EnemyTotalMaxStats / 4f;

                        // Calculaet player stats to compare
                        float PlayerTotalMaxStats = (___playerstatus.AllMaxHP() * HpValuability) + (___playerstatus.AllMaxSP() * SpValuability) + (___playerstatus.AllMaxMP() * MpValuability);
                        float PlayerCurrentStats = (___playerstatus.Hp * HpValuability) + (___playerstatus.Sp * SpValuability) + (___playerstatus.Mp * MpValuability);
                        float PlayerPleasure = ___playerstatus._BadstatusVal[0] * PleasureValuability;
                        float PlayerTotalStats = PlayerTotalMaxStats + PlayerCurrentStats - PlayerPleasure;
                        bool PlayerWeakState = PlayerCurrentStats < PlayerTotalMaxStats / 4f;

                        // Compare enemy and palyer
                        // Smaller coeficient will give stronger sp regeneration
                        float StatsCoefCompared = (EnemyTotalStats / PlayerTotalStats);
                        SpRegenWhenDowned *= StatsCoefCompared;

                        // Who is stronger
                        bool EnemyStronger = EnemyTotalStats * 0.7f > PlayerTotalStats;
                        bool PlayerStronger = PlayerTotalStats * 0.7f > EnemyTotalStats;

                        // Bonuses and debufs
                        bool buf_01 = PlayerStronger && EmenyWeakState;
                        bool buf_02 = PlayerStronger;
                        bool buf_03 = EmenyWeakState;

                        bool debuf_01 = EnemyStronger && PlayerWeakState;
                        bool debuf_02 = EnemyStronger;
                        bool debuf_03 = PlayerWeakState;

                        if (buf_01)
                        {
                            SpRegenWhenDowned /= 1.4f;
                        }
                        if (buf_02)
                        {
                            SpRegenWhenDowned /= 1.4f;
                        }
                        if (buf_03)
                        {
                            SpRegenWhenDowned /= 1.4f;
                        }
                        if (debuf_01)
                        {
                            SpRegenWhenDowned *= 1.4f;
                        }
                        if (debuf_02)
                        {
                            SpRegenWhenDowned *= 1.4f;
                        }
                        if (debuf_03)
                        {
                            SpRegenWhenDowned *= 1.4f;
                        }
                    }
                }
                Plugin.LoggerMessage03 = "FinalRegenWhenDowned" + (___playerstatus.AllMaxSP() / SpRegenWhenDowned * global::UnityEngine.Time.deltaTime).ToString();
                ___playerstatus.Sp += ___playerstatus.AllMaxSP() / SpRegenWhenDowned * global::UnityEngine.Time.deltaTime;

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
            float RegenerationStrength = Plugin.RegenerationFromSource(TotalRegenSource);

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
