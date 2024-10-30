
using UnityEngine;

namespace ProtoWorld
{
    public enum MiscType
    {
        None = 0,
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Misc", menuName = "Inventory System/Items/Misc")]
    public class MiscData : ItemData
    {
        public MiscType miscType;
        public MiscData()
        {
            itemType = ItemType.Misc;
        }
    }
}