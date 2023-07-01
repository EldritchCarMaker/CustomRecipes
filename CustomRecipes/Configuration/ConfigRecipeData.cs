using Nautilus.Crafting;
using Newtonsoft.Json;
using SharedProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CraftData;

namespace CustomRecipes.Configuration
{
    internal class ConfigRecipeData
    {
        public static implicit operator RecipeData(ConfigRecipeData configRecipeData)
        {
            RecipeData data = new RecipeData();
            data.Ingredients = new();
            configRecipeData.ingredients.ForEach(item => data.Ingredients.Add(item));
            data.craftAmount = configRecipeData.craftAmount;
            data.LinkedItems = new();
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
