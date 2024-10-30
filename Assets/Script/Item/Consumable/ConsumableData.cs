
using UnityEngine;

namespace ProtoWorld
{
    public enum ComsumableType
    {
        Food,
        Beverage,
    }

    [System.Serializable]
    public abstract class ConsumableData : ItemData
    {
        public ComsumableType consumableType;
        public ConsumableData()
        {
            itemType = ItemType.Consumable;
        }
    }
}