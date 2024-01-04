using UnityEngine;

namespace NoRImmersiveEroMod
{
    [global::BepInEx.BepInPlugin("NoRImmersiveEroMod", "[twitter @Dru9Dealer] NoR_ImmersiveEroMod", "0.3.0")]
    [global::BepInEx.BepInProcess("NightofRevenge.exe")]
    public class Plugin : global::BepInEx.BaseUnityPlugin
    {
        private void Awake()
        {
            Log = base.Logger;
            // Enemies
            eliteSpawnChance = base.Config.Bind<float>("Enemies", "SpawnChance", 0.3f, "Chance for an enemy to spawn as an elite (0-1)");
            eliteMinHPMulti = base.Config.Bind<float>("Enemies", "HPMultiplier", 0.5f, "Enemies have random Hp multiplier from MinHPMultiplier to MaxHPMultiplier value acceptable range (0.1- 1)");
            eliteMaxHPMulti = base.Config.Bind<float>("Enemies", "HPMultiplier", 4f, "Enemies have random Hp multiplier from MinHPMultiplier to MaxHPMultiplier value acceptable range (1-10)");
            eliteEXPMulti = base.Config.Bind<float>("Enemies", "EXPMultiplier", 4f, "Elites have their EXP multiplied by this value");
            eliteSpeedMulti = base.Config.Bind<float>("Enemies", "SpeedMultiplier", 1.5f, "Elite enemies ATK multiplier random range (0.5 - SpeedMultiplier)");
            eliteColor = base.Config.Bind<string>("Enemies", "Color", "#ffffff", "Elites cab be tinted this color (#550055), by default color is transparent");
            CanEliteReganerate = base.Config.Bind<bool>("Enemies", "CanEnemyReganerate", true, "Enemy will regenerate some hp after hit player");
            EnemyRegenerationMultiplier = base.Config.Bind<float>("Enemies", "EnemyRegenerationMultiplier", 0.01f, "Enemy will regenerate some hp * EnemyRegenerationMultiplier acceptable range(0.01 to 2)");
            // Pleasure
            pleasureEnemyAttackMin = base.Config.Bind<float>("PleasureStatus", "EnemyAttackMultiplierMin", 1f, "Player takes this much more damage when at zero pleasure");
            pleasureEnemyAttackMax = base.Config.Bind<float>("PleasureStatus", "EnemyAttackMultiplierMax", 2.0f, "Player takes this much more damage when at max pleasure");
            pleasurePlayerAttackMax = base.Config.Bind<float>("PleasureStatus", "PlayerAttackMultiplierMax", 0.3f, "Player deals this much more damage when at max pleasure");
            pleasurePlayerAttackMin = base.Config.Bind<float>("PleasureStatus", "PlayerAttackMultiplierMin", 1f, "Player deals this much more damage when at zero pleasure");
            pleasureAttackSpeedMax = base.Config.Bind<float>("PleasureStatus", "PlayerAttackSpeedMultiplierMax", 0.7f, "Player attacks this much faster when at max pleasure");
            pleasureAttackSpeedMin = base.Config.Bind<float>("PleasureStatus", "PlayerAttackSpeedMultiplierMin", 1.3f, "Player attacks this much faster when at zero pleasure");
            // Rape
            pleasureOnRapeGainMultiplier = base.Config.Bind<float>("PleasureStatus", "pleasureOnRapeGainMultiplier", 1f, "Pleasure multiplier when rape starts, range (0.1 - 2)");
            SPRegenOnDownState = base.Config.Bind<float>("PleasureStatus", "SPRegenOnDownState", 1f, "Stamina regeneration multiplier, when player knock down range (0.1 - 10)");
            EnemyMinCurrentPlayerHealthDrainOnCum = base.Config.Bind<float>("Rape", "EnemyMinCurrentPlayerHealthDrainOnCum", 0.1f, "Enemi will drain random amount of player current hp based on EnemyMinCurrentPlayerHealthDrainOnCum range (0.1 - 1) 0.1 = 10% of player current hp, works only if enemy wounded");
            EnemyMaxCurrentPlayerHealthDrainOnCum = base.Config.Bind<float>("Rape", "EnemyMaxCurrentPlayerHealthDrainOnCum", 0.5f, "Enemi will drain random amount of player current hp based on EnemyMaxCurrentPlayerHealthDrainOnCum range (0.1 - 1) 1.0 = 100% of player current hp, works only if enemy wounded");
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
        // Enemies configs
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteSpawnChance;
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteMinHPMulti;
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteMaxHPMulti;
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteEXPMulti;
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteSpeedMulti;
        public static global::BepInEx.Configuration.ConfigEntry<string> eliteColor;
        public static BepInEx.Configuration.ConfigEntry<bool> CanEliteReganerate;
        public static global::BepInEx.Configuration.ConfigEntry<float> EnemyRegenerationMultiplier;
        // Rape
        public static global::BepInEx.Configuration.ConfigEntry<float> EnemyMinCurrentPlayerHealthDrainOnCum;
        public static global::BepInEx.Configuration.ConfigEntry<float> EnemyMaxCurrentPlayerHealthDrainOnCum;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureOnRapeGainMultiplier;
        public static global::BepInEx.Configuration.ConfigEntry<float> SPRegenOnDownState;
        // Pleasure configs
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureEnemyAttackMax;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureEnemyAttackMin;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasurePlayerAttackMax;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasurePlayerAttackMin;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureAttackSpeedMax;
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureAttackSpeedMin;
        internal static global::BepInEx.Logging.ManualLogSource Log;

        // Basic stats and states calculation
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

