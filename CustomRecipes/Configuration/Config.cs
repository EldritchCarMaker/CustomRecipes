using Nautilus.Crafting;
using Nautilus.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static CraftData;

namespace CustomRecipes.Configuration
{
    internal class Config : ConfigFile
    {
        public Dictionary<string, ConfigRecipeData> modifiedRecipes = new()
        {
            {
                TechType.Battery.ToString(),
                new ConfigRecipeData(new ConfigRecipeData.ConfigIngredient(TechType.AcidMushroom, 2), new ConfigRecipeData.ConfigIngredient(TechType.Copper, 1))
            }
        };
    }
}
