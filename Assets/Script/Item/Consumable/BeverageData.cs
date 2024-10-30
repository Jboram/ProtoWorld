
using UnityEngine;

namespace ProtoWorld
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Beverage", menuName = "Inventory System/Items/Beverage")]
    public class BeverageData : ConsumableData
    {
        public float mpRegen;

        public BeverageData()
        {
            consumableType = ComsumableType.Beverage;
        }
    }
}