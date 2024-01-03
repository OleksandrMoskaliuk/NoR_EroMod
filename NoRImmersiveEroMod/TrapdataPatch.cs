using System;

namespace NoRImmersiveEroMod
{
    internal class TrapdataPatch
    {

        [global::HarmonyLib.HarmonyPatch(typeof(global::Trapdata), "Nakadasi")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void OnCreampie(global::Trapdata __instance, global::PlayerStatus ___playerstatus)
        {
            // On the end enemy will restore it heath and player will get restored amount as damage
            float EnemyHpRecov = Math.Min((__instance.MaxHp - __instance.Hp), (___playerstatus.Hp * UnityEngine.Random.Range(0.1f, 0.9f)));
            __instance.Hp += EnemyHpRecov;
            ___playerstatus.Hp -= EnemyHpRecov;
            ___playerstatus.Sp += UnityEngine.Random.Range((0.5f * ___playerstatus.AllMaxSP()), (1 * ___playerstatus.AllMaxSP())) * Plugin.RapeEscapeDifficulty.Value;
        }

        public TrapdataPatch()
        {
        }
    }
}
