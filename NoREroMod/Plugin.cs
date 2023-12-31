using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace NoREroMod
{
    // Token: 0x02000008 RID: 8
    [global::BepInEx.BepInPlugin("NoREroMod", "NoREroMod", "0.3.0")]
    [global::BepInEx.BepInProcess("NightofRevenge.exe")]
    public class Plugin : global::BepInEx.BaseUnityPlugin
    {
        // Token: 0x06000016 RID: 22 RVA: 0x0000293C File Offset: 0x00000B3C
        private void Awake()
        {
            global::NoREroMod.Plugin.Log = base.Logger;
            global::NoREroMod.Plugin.eliteSpawnChance = base.Config.Bind<float>("Elites", "SpawnChance", 0.1f, "Chance for an enemy to spawn as an elite (0-1)");
            global::NoREroMod.Plugin.eliteHPMulti = base.Config.Bind<float>("Elites", "HPMultiplier", 3f, "Elites have their HP multiplied by this value");
            global::NoREroMod.Plugin.eliteEXPMulti = base.Config.Bind<float>("Elites", "EXPMultiplier", 4f, "Elites have their EXP multiplied by this value");
            global::NoREroMod.Plugin.eliteSpeedMulti = base.Config.Bind<float>("Elites", "SpeedMultiplier", 1.3f, "Elites have their movement and animation speed multiplied by this value");
            global::NoREroMod.Plugin.eliteGrabInvul = base.Config.Bind<float>("Elites", "GrabInvulTime", 1f, "Time in secs in which you can't be downed again by elites after wakeup");
            global::NoREroMod.Plugin.eliteColor = base.Config.Bind<string>("Elites", "Color", "#550055", "Elites will be tinted this color (#RRGGBB)");
            global::NoREroMod.Plugin.pleasureEnemyAttackMax = base.Config.Bind<float>("PleasureStatus", "EnemyAttackMultiplierMax", 2.5f, "Player takes this much more damage when at max pleasure");
            global::NoREroMod.Plugin.pleasureEnemyAttackMin = base.Config.Bind<float>("PleasureStatus", "EnemyAttackMultiplierMin", 1f, "Player takes this much more damage when at zero pleasure");
            global::NoREroMod.Plugin.pleasurePlayerAttackMax = base.Config.Bind<float>("PleasureStatus", "PlayerAttackMultiplierMax", 0.3f, "Player deals this much more damage when at max pleasure");
            global::NoREroMod.Plugin.pleasurePlayerAttackMin = base.Config.Bind<float>("PleasureStatus", "PlayerAttackMultiplierMin", 1f, "Player deals this much more damage when at zero pleasure");
            global::NoREroMod.Plugin.pleasureAttackSpeedMax = base.Config.Bind<float>("PleasureStatus", "PlayerAttackSpeedMultiplierMax", 0.7f, "Player attacks this much faster when at max pleasure");
            global::NoREroMod.Plugin.pleasureAttackSpeedMin = base.Config.Bind<float>("PleasureStatus", "PlayerAttackSpeedMultiplierMin", 1.3f, "Player attacks this much faster when at zero pleasure");
            global::NoREroMod.Plugin.pleasureGainOnEro = base.Config.Bind<float>("PleasureStatus", "GainPerSecDuringEro", 0.5f, "Amount pleasure bar fills per sec during ero (0-100)");
            global::NoREroMod.Plugin.pleasureGainOnDown = base.Config.Bind<float>("PleasureStatus", "GainWhenDowned", 10f, "Amount pleasure bar fills when downed by an attack (0-100)");
            global::NoREroMod.Plugin.pleasureSPRegenMax = base.Config.Bind<float>("PleasureStatus", "SPRegenMax", 50f, "Number of secs to go from 0% to 100% SP when downed at max pleasure");
            global::NoREroMod.Plugin.pleasureSPRegenMin = base.Config.Bind<float>("PleasureStatus", "SPRegenMin", 10f, "Number of secs to go from 0% to 100% SP when downed at zero pleasure");
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoREroMod.PlayerConPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoREroMod.PlayerStatusPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoREroMod.EnemyDatePatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoREroMod.TrapdataPatch), null);
            global::HarmonyLib.Harmony.CreateAndPatchAll(typeof(global::NoREroMod.BuffPatch), null);
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00002BFC File Offset: 0x00000DFC
        private void Update()
        {
            bool flag = global::NoREroMod.Plugin.eliteGrabInvulTimer > 0f;
            if (flag)
            {
                global::NoREroMod.Plugin.eliteGrabInvulTimer -= global::UnityEngine.Time.deltaTime;
            }

        }

        private void OnGUI()
        {
            //global::UnityEngine.GUI.Box(LogDat1.rectangle, "Message");
            HandleLoggers(true);
        }

        private void HandleLoggers( bool on)
        {
            if (!on) { return; }
            // LOGGER_01            
            if (LogDat1.TimeRamained > 0)
            {
            global::UnityEngine.GUI.Box(LogDat1.rectangle, "Log 01: " + LoggerMessage01);
            LogDat1.LastMessage = LoggerMessage01;
            LogDat1.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage01.Equals(LogDat1.LastMessage)) 
            {
                LogDat1.TimeRamained = 5f;
            }
            // LOGGER_02            
            if (LogDat2.TimeRamained > 0)
            {
                global::UnityEngine.GUI.Box(LogDat2.rectangle, "Log 02: " + LoggerMessage02);
                LogDat2.LastMessage = LoggerMessage02;
                LogDat2.TimeRamained -= UnityEngine.Time.deltaTime;
            }
            // Update time if new message was assigned
            if (!LoggerMessage02.Equals(LogDat2.LastMessage))
            {
                LogDat2.TimeRamained = 5f;
            }

        }
        // Token: 0x06000018 RID: 24 RVA: 0x00002106 File Offset: 0x00000306
        public Plugin()
        {
            LogDat1.LastMessage = LoggerMessage01;
            LogDat1.rectangle = new global::UnityEngine.Rect(10f, 300f, 350f, 24f);
            LogDat1.TimeRamained = 0;

            //Step: Rect(10f, 310f + 24f +4f = 338f, 350f, 24f);
            LogDat2.LastMessage = LoggerMessage02;
            LogDat2.rectangle = new global::UnityEngine.Rect(10f, 328f, 350f, 24f);
            LogDat2.TimeRamained = 0f;
        }

        // Token: 0x04000002 RID: 2
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteSpawnChance;

        // Token: 0x04000003 RID: 3
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteHPMulti;

        // Token: 0x04000004 RID: 4
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteEXPMulti;

        // Token: 0x04000005 RID: 5
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteSpeedMulti;

        // Token: 0x04000006 RID: 6
        public static global::BepInEx.Configuration.ConfigEntry<float> eliteGrabInvul;

        // Token: 0x04000007 RID: 7
        public static global::BepInEx.Configuration.ConfigEntry<string> eliteColor;

        // Token: 0x04000008 RID: 8
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureEnemyAttackMax;

        // Token: 0x04000009 RID: 9
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureEnemyAttackMin;

        // Token: 0x0400000A RID: 10
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasurePlayerAttackMax;

        // Token: 0x0400000B RID: 11
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasurePlayerAttackMin;

        // Token: 0x0400000C RID: 12
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureAttackSpeedMax;

        // Token: 0x0400000D RID: 13
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureAttackSpeedMin;

        // Token: 0x0400000E RID: 14
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureGainOnEro;

        // Token: 0x0400000F RID: 15
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureGainOnDown;

        // Token: 0x04000010 RID: 16
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureSPRegenMax;

        // Token: 0x04000011 RID: 17
        public static global::BepInEx.Configuration.ConfigEntry<float> pleasureSPRegenMin;

        // Token: 0x04000012 RID: 18
        public static float eliteGrabInvulTimer;

        // Logger 01
        public static string LoggerMessage01;
        private LogData LogDat1;

        // Logger 02
        public static string LoggerMessage02;
        private LogData LogDat2;

        // Token: 0x04000013 RID: 19
        internal static global::BepInEx.Logging.ManualLogSource Log;


    }
    // Data for Logging messages
    struct LogData
    {
        public Rect rectangle;
        public string LastMessage;
        public float TimeRamained;
    }

}

