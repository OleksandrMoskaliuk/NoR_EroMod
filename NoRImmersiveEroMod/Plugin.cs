using UnityEngine;

namespace NoRImmersiveEroMod
{
    [global::BepInEx.BepInPlugin("NoRImmersiveEroMod", "[twitter @Dru9Dealer] NoR_ImmersiveEroMod", "0.3.0")]
    [global::BepInEx.BepInProcess("NightofRevenge.exe")]
    public class Plugin : global::BepInEx.BaseUnityPlugin
    {
        private void Awake()
        {
            global::NoRImmersiveEroMod.Plugin.Log = base.Logger;
            global::NoRImmersiveEroMod.Plugin.eliteSpawnChance = base.Config.Bind<float>("Enemies", "SpawnChance", 1f, "Chance for an enemy to spawn as an elite (0-1)");
            global::NoRImmersiveEroMod.Plugin.eliteHPMulti = base.Config.Bind<float>("Enemies", "HPMultiplier", 3f, "Enemies have random Hp multiplier from 1 to HPMultiplier value");
            global::NoRImmersiveEroMod.Plugin.eliteEXPMulti = base.Config.Bind<float>("Enemies", "EXPMultiplier", 4f, "Elites have their EXP multiplied by this value");
            global::NoRImmersiveEroMod.Plugin.eliteSpeedMulti = base.Config.Bind<float>("Enemies", "SpeedMultiplier", 1.5f, "Elite enemies ATK multiplier random range (0.5 - SpeedMultiplier)");
            global::NoRImmersiveEroMod.Plugin.eliteColor = base.Config.Bind<string>("Enemies", "Color", "#ffffff", "Elites cab be tinted this color (#550055), by default color is transparent");
            global::NoRImmersiveEroMod.Plugin.pleasureEnemyAttackMax = base.Config.Bind<float>("PleasureStatus", "EnemyAttackMultiplierMax", 2.0f, "Player takes this much more damage when at max pleasure");
            global::NoRImmersiveEroMod.Plugin.pleasureEnemyAttackMin = base.Config.Bind<float>("PleasureStatus", "EnemyAttackMultiplierMin", 1f, "Player takes this much more damage when at zero pleasure");
            global::NoRImmersiveEroMod.Plugin.pleasurePlayerAttackMax = base.Config.Bind<float>("PleasureStatus", "PlayerAttackMultiplierMax", 0.3f, "Player deals this much more damage when at max pleasure");
            global::NoRImmersiveEroMod.Plugin.pleasurePlayerAttackMin = base.Config.Bind<float>("PleasureStatus", "PlayerAttackMultiplierMin", 1f, "Player deals this much more damage when at zero pleasure");
            global::NoRImmersiveEroMod.Plugin.pleasureAttackSpeedMax = base.Config.Bind<float>("PleasureStatus", "PlayerAttackSpeedMultiplierMax", 0.7f, "Player attacks this much faster when at max pleasure");
            global::NoRImmersiveEroMod.Plugin.pleasureAttackSpeedMin = base.Config.Bind<float>("PleasureStatus", "PlayerAttackSpeedMultiplierMin", 1.3f, "Player attacks this much faster when at zero pleasure");
            global::NoRImmersiveEroMod.Plugin.pleasureOnRapeGainMultiplier = base.Config.Bind<float>("PleasureStatus", "pleasureOnRapeGainMultiplier", 1f, "Pleasure multiplier when rape starts, range (0.1 - 2)");
            global::NoRImmersiveEroMod.Plugin.SPRegenOnDownState = base.Config.Bind<float>("PleasureStatus", "SPRegenOnDownState", 1f, "Stamina regeneration multiplier, when player knock down range (0.1 - 10)");
            global::NoRImmersiveEroMod.Plugin.RapeEscapeDifficulty = base.Config.Bind<float>("Rape", "RapeEscapeDifficulty", 1f, "Rape Escape multiplier, by default 1 in default range (0.1 - 2)");
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoRImmersiveEroMod.PlayerConPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoRImmersiveEroMod.PlayerStatusPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoRImmersiveEroMod.EnemyDatePatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoRImmersiveEroMod.TrapdataPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoRImmersiveEroMod.BuffPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoRImmersiveEroMod.SlavehelpPatch), null);
        }

        private void Update()
        {

        }

        private void OnGUI()
        {
            HandleLoggers(true);
        }

        private void HandleLoggers(bool on)
        {
            if (!on) { return; }

            // Logger 01            
            if (LogDat1.TimeRamained > 0)
            {
                global::UnityEngine.GUI.Box(LogDat1.rectangle, "Log 01: " + LoggerMessage01);
                LogDat1.LastMessage = LoggerMessage01;
                LogDat1.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage01.Equals(LogDat1.LastMessage))
            {
                // Prevents messages flickering
                if (LogDat1.TimeRamained > (5f - 0.8f))
                {
                    LoggerMessage01 = LogDat1.LastMessage;
                }
                else
                {
                    LogDat1.TimeRamained = 5f;
                }
            }

            // Logger 02            
            if (LogDat2.TimeRamained > 0)
            {
                global::UnityEngine.GUI.Box(LogDat2.rectangle, "Log 02: " + LoggerMessage02);
                LogDat2.LastMessage = LoggerMessage02;
                LogDat2.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage02.Equals(LogDat2.LastMessage))
            {
                // Prevents messages flickering
                if (LogDat2.TimeRamained > (5f - 0.8f))
                {
                    LoggerMessage02 = LogDat2.LastMessage;
                }
                else
                {
                    LogDat2.TimeRamained = 5f;
                }
            }

            // Logger 03           
            if (LogDat3.TimeRamained > 0)
            {
                global::UnityEngine.GUI.Box(LogDat3.rectangle, "Log 03: " + LoggerMessage03);
                LogDat3.LastMessage = LoggerMessage03;
                LogDat3.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage03.Equals(LogDat3.LastMessage))
            {
                // Prevents messages flickering
                if (LogDat3.TimeRamained > (5f - 0.8f))
                {
                    LoggerMessage03 = LogDat3.LastMessage;
                }
                else
                {
                    LogDat3.TimeRamained = 5f;
                }
            }

            // Logger 04            
            if (LogDat4.TimeRamained > 0)
            {
                global::UnityEngine.GUI.Box(LogDat4.rectangle, "Log 04: " + LoggerMessage04);
                LogDat4.LastMessage = LoggerMessage04;
                LogDat4.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage04.Equals(LogDat4.LastMessage))
            {
                // Prevents messages flickering
                if (LogDat4.TimeRamained > (5f - 0.8f))
                {
                    LoggerMessage04 = LogDat4.LastMessage;
                }
                else
                {
                    LogDat4.TimeRamained = 5f;
                }
            }
        }
        public Plugin()
        {
            float YPoint = 300;
            float Step = 24f + 4f;

            // Logger 01
            LogDat1.LastMessage = LoggerMessage01;
            LogDat1.rectangle = new global::UnityEngine.Rect(10f, YPoint, 350f, 24f);

            // Logger 02
            //Step: Rect(10f, 310f + 24f +4f = 338f, 350f, 24f);
            LogDat2.LastMessage = LoggerMessage02;
            LogDat2.rectangle = new global::UnityEngine.Rect(10f, YPoint += Step, 350f, 24f);

            // Logger 03
            LogDat3.LastMessage = LoggerMessage03;
            LogDat3.rectangle = new global::UnityEngine.Rect(10f, YPoint += Step, 350f, 24f);

            // Logger 04
            LogDat4.LastMessage = LoggerMessage04;
            LogDat4.rectangle = new global::UnityEngine.Rect(10f, YPoint += Step, 350f, 24f);

            gameplay = new GameplayInfo();
        }

        public static global::BepInEx.Configuration.ConfigEntry<float> eliteSpawnChance;
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteHPMulti;
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteEXPMulti;
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteSpeedMulti;
        public static global::BepInEx.Configuration.ConfigEntry<string> eliteColor;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureEnemyAttackMax;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureEnemyAttackMin;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasurePlayerAttackMax;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasurePlayerAttackMin;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureAttackSpeedMax;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureAttackSpeedMin;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureOnRapeGainMultiplier;
        public static global::BepInEx.Configuration.ConfigEntry<float> SPRegenOnDownState;
        public static global::BepInEx.Configuration.ConfigEntry<float> RapeEscapeDifficulty;
        internal static global::BepInEx.Logging.ManualLogSource Log;
        public static GameplayInfo gameplay;

        // Logger 01
        public static string LoggerMessage01;
        private LogData LogDat1;

        // Logger 02
        public static string LoggerMessage02;
        private LogData LogDat2;

        // Logger 03
        public static string LoggerMessage03;
        private LogData LogDat3;

        // Logger 04
        public static string LoggerMessage04;
        private LogData LogDat4;

    }

    // Data for Logging messages
    public struct LogData
    {
        public Rect rectangle;
        public string LastMessage;
        public float TimeRamained;
    }

}

