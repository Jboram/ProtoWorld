
using UnityEngine;

namespace ProtoWorld
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
    }

    [System.Serializable]
    public abstract class EquipmentData : ItemData
    {
        public EquipmentType equipmentType;

        public EquipmentData()
        {
            itemType = ItemType.Equipment;
        }
    }
}