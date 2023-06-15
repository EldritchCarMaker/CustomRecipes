using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Handlers;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace CustomRecipes
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }

        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        internal new static Config Config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

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
            yield return new WaitForSeconds(10);//idfk random number just here so we can get 99% of modded techtypes
            //not actually really necessary, most modded types are declared in Awake anyway
            //but better to have than not have
            Config.modifiedRecipes.ForEach(recipe => CraftDataHandler.SetRecipeData(recipe.Key.AsEnum(), recipe.Value));
        }
    }
    public static class Extension
    {
        public static TechType AsEnum(this string str)
        {
            return AsEnum<TechType>(str);
        }
        public static T AsEnum<T>(this string str) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), str);
        }
    }
}