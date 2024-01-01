using System;
using HarmonyLib;
using Rewired;
using Spine.Unity;
using UnityEngine;

namespace NoREroMod
{
    // Token: 0x02000005 RID: 5
    internal class EnemyDatePatch
    {
        // Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
        [global::HarmonyLib.HarmonyPatch(typeof(global::EnemyDate), "Nakadasi")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void BirthOnCreampie(global::EnemyDate __instance, global::PlayerStatus ___playerstatus)
        {
            bool flag = ___playerstatus._BadstatusVal[2] <= 0f && ___playerstatus._BadstatusVal[3] <= 0f;
            if (flag)
            {
                ___playerstatus.CreampieVal_UI();
                __instance.com_player._BirthNumber = 1;
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x0000207C File Offset: 0x0000027C
        [global::HarmonyLib.HarmonyPatch(typeof(global::EnemyDate), "WeaponDamage")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::EnemyDate), "StabDamage")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void GainMPOnHit(global::PlayerStatus ___playerstatus)
        {
            if (___playerstatus.correction[2] > 0f)
            {
                ___playerstatus.Mp += 3f * ___playerstatus.correction[2];
            }
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00002188 File Offset: 0x00000388
        [global::HarmonyLib.HarmonyPatch(typeof(global::Arulaune), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BigMerman), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Bigoni), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackMafia), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackOoze_Monster), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Cocoonman), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Coolmaiden), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingCreatures), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingDead), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingSisterKnight), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrowInquisition), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::goblin), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Gorotuki), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::HighInquisition_famale), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Inquisition), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionRED), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionWhite), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Kinoko), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Librarian), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mafiamuscle), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Minotaurosu), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyDog), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyMan), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mutude), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::OtherSlavebigAxe), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Pilgrim), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::PrisonOfficer), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::RequiemKnight), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sheepheaddemon), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SinnerslaveCrossbow), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisiruirui), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisterknight), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SkeltonOoze), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Slaughterer), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SlaveBigAxe), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Snailshell), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::suraimu), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuAxe), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuNormal), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Tyoukyoushi), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TyoukyoushiRed), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Undead), "Start")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Vagrant), "Start")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void SpawnSuperEnemy(global::EnemyDate __instance, global::Spine.Unity.SkeletonAnimation ___mySpine, ref string ___JPname)
        {
            bool flag = global::UnityEngine.Random.value < global::NoREroMod.Plugin.eliteSpawnChance.Value;
            if (flag)
            {
                ___JPname += "<SUPER>";
                __instance.MaxHp *= global::NoREroMod.Plugin.eliteHPMulti.Value;
                __instance.Hp = __instance.MaxHp;
                __instance.Exp = global::UnityEngine.Mathf.RoundToInt((float)__instance.Exp * global::NoREroMod.Plugin.eliteEXPMulti.Value);
                __instance.enmMovespeed *= global::NoREroMod.Plugin.eliteSpeedMulti.Value;
                global::UnityEngine.Color color;
                bool flag2 = global::UnityEngine.ColorUtility.TryParseHtmlString(global::NoREroMod.Plugin.eliteColor.Value, out color);
                if (flag2)
                {
                    ___mySpine.skeleton.SetColor(color);
                }
            }
        }

        // Token: 0x06000009 RID: 9 RVA: 0x0000223C File Offset: 0x0000043C
        [global::HarmonyLib.HarmonyPatch(typeof(global::Arulaune), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BigMerman), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Bigoni), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackMafia), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackOoze_Monster), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Cocoonman), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Coolmaiden), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingCreatures), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingDead), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingSisterKnight), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrowInquisition), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::goblin), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Gorotuki), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::HighInquisition_famale), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Inquisition), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionRED), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionWhite), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Kinoko), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Librarian), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mafiamuscle), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Minotaurosu), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyDog), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyMan), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mutude), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::OtherSlavebigAxe), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Pilgrim), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::PrisonOfficer), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::RequiemKnight), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sheepheaddemon), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SinnerslaveCrossbow), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisiruirui), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisterknight), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SkeltonOoze), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Slaughterer), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SlaveBigAxe), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Snailshell), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::suraimu), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuAxe), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuNormal), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Tyoukyoushi), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TyoukyoushiRed), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Undead), "reste")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Vagrant), "reste")]
        [global::HarmonyLib.HarmonyPrefix]
        private static bool SuperEnemyColor(global::EnemyDate __instance, global::Spine.Unity.SkeletonAnimation ___mySpine, string ___JPname)
        {
            bool flag = ___JPname.Contains("<SUPER>");
            if (flag)
            {
                global::UnityEngine.Color color;
                bool flag2 = global::UnityEngine.ColorUtility.TryParseHtmlString(global::NoREroMod.Plugin.eliteColor.Value, out color);
                if (flag2)
                {
                    ___mySpine.skeleton.SetColor(color);
                    return false;
                }
            }
            return true;
        }

        // Token: 0x0600000A RID: 10 RVA: 0x00002288 File Offset: 0x00000488
        [global::HarmonyLib.HarmonyPatch(typeof(global::Arulaune), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BigMerman), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Bigoni), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackMafia), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackOoze_Monster), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Cocoonman), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Coolmaiden), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingCreatures), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingDead), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingSisterKnight), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrowInquisition), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::goblin), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Gorotuki), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::HighInquisition_famale), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Inquisition), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionRED), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionWhite), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Kinoko), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Librarian), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mafiamuscle), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Minotaurosu), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyDog), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyMan), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mutude), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::OtherSlavebigAxe), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Pilgrim), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::PrisonOfficer), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::RequiemKnight), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sheepheaddemon), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SinnerslaveCrossbow), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisiruirui), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisterknight), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SkeltonOoze), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Slaughterer), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SlaveBigAxe), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Snailshell), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::suraimu), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuAxe), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuNormal), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Tyoukyoushi), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TyoukyoushiRed), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Undead), "fun_animekind")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Vagrant), "fun_animekind")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void SuperEnemySpeed(string ___JPname, ref float ___imagetime)
        {
            bool flag = ___JPname.Contains("<SUPER>");
            if (flag)
            {
                ___imagetime *= global::NoREroMod.Plugin.eliteSpeedMulti.Value;
            }
        }

        // Token: 0x0600000B RID: 11 RVA: 0x000022B8 File Offset: 0x000004B8
        [global::HarmonyLib.HarmonyPatch(typeof(global::Arulaune), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BigMerman), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Bigoni), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackMafia), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::BlackOoze_Monster), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Cocoonman), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Coolmaiden), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingCreatures), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingDead), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrawlingSisterKnight), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::CrowInquisition), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::goblin), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Gorotuki), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::HighInquisition_famale), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Inquisition), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionRED), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::InquisitionWhite), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Kinoko), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Librarian), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mafiamuscle), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Minotaurosu), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyDog), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::MummyMan), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Mutude), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::OtherSlavebigAxe), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Pilgrim), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::PrisonOfficer), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::RequiemKnight), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sheepheaddemon), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SinnerslaveCrossbow), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisiruirui), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Sisterknight), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SkeltonOoze), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Slaughterer), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::SlaveBigAxe), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Snailshell), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::suraimu), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuAxe), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TouzokuNormal), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Tyoukyoushi), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::TyoukyoushiRed), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Undead), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::Vagrant), "OnTriggerStay2D")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void GrabPlayerOnTouch(global::EnemyDate __instance, global::UnityEngine.Collider2D collision,
            global::PlayerStatus ___playerstatus, string ___JPname, ref bool ___ParryBlank,
            float ___piyoriY, global::UnityEngine.GameObject ___piyo)
        {
            // Plugin assignment, needed for stamina on erodown
            Plugin.EneymyData = __instance;
            // Check if player was damaged using  hp bar
            global::UnityEngine.GameObject obj_playerUI = global::UnityEngine.GameObject.Find("UI");
            global::UImng playerUI = obj_playerUI.GetComponent<global::UImng>();
            bool IsHit = playerUI.hpbarback.fillAmount > ___playerstatus.Hp / ___playerstatus.AllMaxHP() + 0.01f;

            // Trigger when player gets damage 
            bool IsDamageStatus = __instance.com_player.state == playercon.DAMAGE;
            // Trigger when player deal damage
          


            // Calculate player stats
            global::Rewired.Player player = global::Rewired.ReInput.players.GetPlayer(__instance.com_player.playerId);
            bool PlayerOnGuard = player.GetButton("Guard");
            bool IsPlayerAttack = player.GetButtonDown("Attack");
            bool IsSubmitKeyPressed = player.GetButtonDown("Submit");
            float PlayerTotalMaxSpHp = ___playerstatus.MaxSp + ___playerstatus.MaxHp;
            float PlayerCurrentSpHp = ___playerstatus.Sp + ___playerstatus.Hp;
            float PlayerTotalStats = PlayerTotalMaxSpHp + PlayerCurrentSpHp;
            bool PlayerWeakState =  PlayerCurrentSpHp < (PlayerTotalMaxSpHp /4f);

            // Calculate enemy stats
            bool EmpoweredEnemy = ___JPname.Contains("<SUPER>");
            float EnemyTotalMaxSpHp = __instance.MaxHp + __instance.MaxSp;
            float EnemyCurrentSpHp = __instance.Sp + __instance.Hp;
            float EnemyTotalStats = EnemyTotalMaxSpHp + EnemyCurrentSpHp;
            bool EmenyWeakState = EnemyCurrentSpHp < (EnemyTotalMaxSpHp / 4f);

            // Calculate if Enemy can  knock down player Using total stats
            bool EnemyStronger = EnemyTotalStats * 0.7f  >  PlayerTotalStats;

            //Plugin.LoggerMessage01 = "Sp+Mp: " + PlayerCurrentSpHp + " SpMpMax/4: " + (PlayerTotalStats/ 4f).ToString();
            //Plugin.LoggerMessage01 = "En_strength * 0.7: " + (EnemyTotalStats * 0.7f).ToString() + " Pl_strength: " + PlayerTotalStats;
            //Plugin.LoggerMessage02 = "Sp+Mp: " + PlayerCurrentSpHp + " SpMpMax/4: " + (PlayerTotalStats / 4f).ToString();
            //Plugin.LoggerMessage03 = "En_strength * 0.7: " + (EnemyTotalStats * 0.7f).ToString() + " Pl_strength: " + PlayerTotalStats;
            //Plugin.LoggerMessage04 = "__instance.enmTough: " + __instance.enmTough;
            //Plugin.LoggerMessage03 = "player.state " + __instance.com_player.state;

            // Calculete condition where eney can knock down player
            bool cnd_01 = IsHit && PlayerWeakState && !EmenyWeakState && IsDamageStatus;
            bool cnd_02 = IsHit && EnemyStronger && IsDamageStatus;
            if (cnd_01 || cnd_02)
            {
                bool Condition = !__instance.com_player.eroflag && !__instance.eroflag && collision.gameObject.tag == "playerDAMAGEcol"
                    && !__instance.com_player.stepfrag && __instance.com_player.m_Grounded;
                if (Condition)
                {
                    __instance.com_player.ImmediatelyERO();
                    ___playerstatus.Sp = 0f;
                }
            }

            // Calculate whether palyer can execute enemy
            bool PlayerStronger = PlayerTotalStats * 0.7 > EnemyTotalStats;
            bool cnd_03 = !IsSubmitKeyPressed && IsPlayerAttack && !PlayerOnGuard && !PlayerWeakState && EmenyWeakState; 
            bool cnd_04 = !IsSubmitKeyPressed && IsPlayerAttack && !PlayerOnGuard && !PlayerWeakState && PlayerStronger;
            // Fatality when enemy weak
            if (cnd_03 || cnd_04)
            {
                Plugin.LoggerMessage03 = "Fatality!!!";
                Plugin.LoggerMessage03 = "Fatality!!";
                __instance.com_player.state = "PARRY";
                __instance.enmTough -= 999;
                __instance.enmMAXfaltertime = 2.3f;
                __instance.enmfaltertime = 1f;
                if (__instance.transform.position.x > __instance.playerPos.x)
                {
                    __instance.damedir = -1;
                }
                else
                {
                    __instance.damedir = 1;
                }
                ___ParryBlank = true;
            }
            
            
        }

        public EnemyDatePatch()
        {
        }
    }
}
