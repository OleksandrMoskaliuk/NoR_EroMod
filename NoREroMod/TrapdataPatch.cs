using System;
using HarmonyLib;
using UnityEngine.SocialPlatforms;

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
            //if (___playerstatus._BadstatusVal[2] <= 100f && ___playerstatus._BadstatusVal[3] <= 0f)
            //{
            //	___playerstatus.CreampieVal_UI();
            //	__instance.com_player._BirthNumber = UnityEngine.Random.Range(0, 2);
            //  }
            if (Plugin.EneymyData != null)
            {
                // After cum, enemy lose sp but regenerete some health
                Plugin.EneymyData.Sp -= Plugin.EneymyData.MaxSp * 0.9f;
                Plugin.EneymyData.Hp += Plugin.EneymyData.MaxHp * 0.2f;
            }
        }

        // Token: 0x0600001A RID: 26 RVA: 0x00002073 File Offset: 0x00000273
        public TrapdataPatch()
        {
        }
    }
}
