
using UnityEngine;

namespace ProtoWorld
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Food", menuName = "Inventory System/Items/Food")]
    public class FoodData : ConsumableData
    {
        public float hpRegen;

        public FoodData()
        {
            consumableType = ComsumableType.Food;
        }
    }
}