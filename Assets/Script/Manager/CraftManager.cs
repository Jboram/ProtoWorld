using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld
{
    public class CraftManager : SingletonBase<CraftManager>
    {
        private List<Recipe> recipes;
        private HashSet<ItemData> prevMadeItem = new HashSet<ItemData>();
        protected override void OnCreated()
        {
            recipes = new List<Recipe>(Resources.LoadAll<Recipe>("Recipes"));
        }

        public Item Craft(Item item1, Item item2)
        {
            foreach (Recipe recipe in recipes)
            {
                if (recipe.CanMade(item1, item2))
                {
                    if (prevMadeItem.Contains(recipe.result) == false)
                    {
                        prevMadeItem.Add(recipe.result);
                    }
                    return new Item(recipe.result, recipe.amount);
                }
            }

            return null;
        }

        public Recipe GetResultRecipe(ItemData ingredient1, ItemData ingredient2)
        {
            foreach (Recipe recipe in recipes)
            {
                if (recipe.Matches(ingredient1, ingredient2))
                {
                    return recipe;
                }
            }

            return null;
        }

        public bool IsMade(ItemData itemData)
        {
            return prevMadeItem.Contains(itemData);
        }
    }

}
