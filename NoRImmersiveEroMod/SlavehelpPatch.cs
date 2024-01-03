using System;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace NoRImmersiveEroMod
{
    internal class SlavehelpPatch
    {
        // Slave always respawn
        [HarmonyLib.HarmonyPatch(typeof(global::Slavehelp), "help")]
        [HarmonyLib.HarmonyPostfix]
        private static void RespawnSlavesAfterHelp(Slavehelp __instance, game_fragmng ___Flagmng, SpawnSlave ___spawnslave)
        {
            ___Flagmng._helpslaveStage[___spawnslave._stage, ___spawnslave._SlaveNumber] = false;
        }

        public SlavehelpPatch()
        {
        }
    }

}
