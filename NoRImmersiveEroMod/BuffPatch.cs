namespace NoRImmersiveEroMod
{

    internal class BuffPatch
    {

        [global::HarmonyLib.HarmonyPatch(typeof(global::Buff), "PleasureParalysis")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void NoPleasureRecovery(global::Buff __instance, bool ___Pleasureparalysis, global::PlayerStatus ___pl, ref float ___CountUp)
        {
            //___CountUp = 0f;
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::Buff), "ParalysisOrgasm")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void NoPleasureRecoveryOnOrgasm(global::Buff __instance, bool ___orgasm)
        {
            //if (___orgasm)
            //{
            //	__instance.Invoke("ParalysisOrgasmInvoke", 2f);
            //}
        }

        public BuffPatch()
        {
        }
    }
}
