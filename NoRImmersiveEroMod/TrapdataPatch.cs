using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace NoRImmersiveEroMod
{
    internal class TrapdataPatch
    {

        [global::HarmonyLib.HarmonyPatch(typeof(global::Trapdata), "Nakadasi")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void BirthOnCreampie(global::Trapdata __instance, global::PlayerStatus ___playerstatus)
        {
            if (___playerstatus._BadstatusVal[2] <= 0 && ___playerstatus._BadstatusVal[3] <= 0f)
            {
                // On the end enemy will restore it heath and player will get restored amount as damage
                float EnemyHpRecov = Math.Min((__instance.MaxHp - __instance.Hp), (___playerstatus.Hp * 0.5f));
                __instance.Hp += EnemyHpRecov* Time.deltaTime;
                __instance.Sp -= __instance.Sp * 0.5f * Time.deltaTime;

                ___playerstatus.Hp -= EnemyHpRecov * Time.deltaTime;
                ___playerstatus.Sp += __instance.Sp * 0.5f * Time.deltaTime;
                ___playerstatus.CreampieVal_UI();
                __instance.com_player._BirthNumber = UnityEngine.Random.Range(0, 1);
                // some sp recovery to escape
            }
        }

        public TrapdataPatch()
        {
        }
    }
}
