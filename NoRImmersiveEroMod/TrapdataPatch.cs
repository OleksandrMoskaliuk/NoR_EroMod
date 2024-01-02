using System;
using HarmonyLib;
using UnityEngine.SocialPlatforms;

namespace NoRImmersiveEroMod
{
    internal class TrapdataPatch
    {

        [global::HarmonyLib.HarmonyPatch(typeof(global::Trapdata), "Nakadasi")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void BirthOnCreampie(global::Trapdata __instance, global::PlayerStatus ___playerstatus)
        {
            //if (___playerstatus._BadstatusVal[2] <= 100f && ___playerstatus._BadstatusVal[3] <= 0f)
            //{
            //	___playerstatus.CreampieVal_UI();
            //	__instance.com_player._BirthNumber = UnityEngine.Random.Range(0, 2);
            //  }
            if (Plugin.EneymyData != null)
            {
                // After cum, enemy lose Sp
                Plugin.EneymyData.Sp -= Plugin.EneymyData.MaxSp * 0.9f;

                // On the end enemy will restore it heath and player will get restored amount as damage
                Plugin.EneymyData.Hp += Plugin.EneymyData.MaxHp * 0.2f;
                ___playerstatus.Hp -= Plugin.EneymyData.MaxHp * 0.2f;
            }
        }

        public TrapdataPatch()
        {
        }
    }
}
