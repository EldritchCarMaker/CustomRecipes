using Nautilus.Crafting;
using Nautilus.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static CraftData;

namespace CustomRecipes
{
    internal class Config : ConfigFile
    {
        public Dictionary<string, ConfigRecipeData> modifiedRecipes = new()
        {
            { 
                TechType.Battery.ToString(), 
                new ConfigRecipeData(new ConfigRecipeData.ConfigIngredient(TechType.AcidMushroom, 5), new ConfigRecipeData.ConfigIngredient(TechType.Copper, 2)) 
            }
        };
    }
    internal class ConfigRecipeData
    {
        public static implicit operator RecipeData(ConfigRecipeData configRecipeData)
        {
            RecipeData data = new RecipeData();
            data.Ingredients = new();
            configRecipeData.ingredients.ForEach(item => data.Ingredients.Add(item));
            data.craftAmount = configRecipeData.craftAmount;
            data.LinkedItems = new ();
            configRecipeData.linkedItems.ForEach(item => data.LinkedItems.Add(item.AsEnum()));

            return data;
        }

        [JsonConstructor()]
        public ConfigRecipeData() { }

        public ConfigRecipeData(params ConfigIngredient[] ingredients)
        {
            this.ingredients.AddRange(ingredients);
        }

        public int craftAmount = 1;
        public List<ConfigIngredient> ingredients = new();
        public List<string> linkedItems = new();


        public class ConfigIngredient
        {
            [JsonConstructor()]
            public ConfigIngredient() { }
            public ConfigIngredient(TechType type, int count = 1)
            {
                techType = type.ToString();
                this.count = count;
            }
            public ConfigIngredient(string type, int count = 1)
            {
                techType = type;
                this.count = count;
            }
            public string techType = "None";
            public int count = 0;

            public static implicit operator Ingredient(ConfigIngredient configIngredient)
            {
                return new Ingredient(configIngredient.techType.AsEnum(), configIngredient.count);
            }
        }
    }
}
