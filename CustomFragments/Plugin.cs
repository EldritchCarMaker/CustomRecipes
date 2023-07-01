using BepInEx;
using BepInEx.Logging;
using CustomFragments.Configuration;
using HarmonyLib;
using Nautilus.Handlers;
using SharedProject;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CustomFragments
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }

        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        private static new Config Config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

        private void Awake()
        {
            // set project-scoped logger instance
            Logger = base.Logger;


            // register harmony patches, if there are any
            Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(10);

            Config.modifiedFragmentCounts.ForEach(pair => HandleModifiedFragments(pair));
        }
        internal static void HandleModifiedFragments(KeyValuePair<string, ConfigFragmentData> pair)
        {
            var type = pair.Key.AsEnum();

            PDAHandler.EditFragmentsToScan(type, pair.Value.fragmentCount);

            if(pair.Value.scanTime >= 0)//ignore negative values, like -1, these will remain default
                PDAHandler.EditFragmentScanTime(type, pair.Value.scanTime);
        }
    }
}