﻿using BepInEx;
using BepInEx.Logging;
using CustomRecipes.Configuration;
using HarmonyLib;
using Nautilus.Handlers;
using SharedProject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using PluginInfo = CustomRecipes.MyPluginInfo;

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
            Config.modifiedRecipes.ForEach(recipe => HandleRecipe(recipe));
        }
        private static void HandleRecipe(KeyValuePair<string, ConfigRecipeData> recipe)
        {
            CraftDataHandler.SetRecipeData(recipe.Key.AsEnum(), recipe.Value);
        }
    }
}