using UnityEngine;

namespace ProtoWorld
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
    [System.Serializable]
    public class Recipe : ScriptableObject
    {
        public ItemData ingredient1;
        public ItemData ingredient2;
        public ItemData result;
        public int amount1 = 1;
        public int amount2 = 1;
        public int amount = 1;

        public bool Matches(ItemData ingredient1, ItemData ingredient2)
        {
            return (this.ingredient1 == ingredient1 && this.ingredient2 == ingredient2) 
                || (this.ingredient1 == ingredient2 && this.ingredient2 == ingredient1);
        }

        public bool CanMade(Item item1, Item item2)
        {
            if (ingredient1 == item1.ItemData && ingredient2 == item2.ItemData)
            {
                return amount1 <= item1.Amount && amount2 <= item2.Amount;
            }
            else if (ingredient1 == item2.ItemData && ingredient2 == item1.ItemData)
            {
                return amount1 <= item2.Amount && amount2 <= item1.Amount;
            }
            return false;
        }
    }
}