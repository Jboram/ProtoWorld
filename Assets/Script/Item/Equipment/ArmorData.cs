using UnityEngine;

namespace ProtoWorld
{
    public enum ArmorType
    {
        Shiled,
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Armor", menuName = "Inventory System/Items/Armor")]
    public class ArmorData : EquipmentData
    {
        public float def;

        public ArmorData()
        {
            equipmentType = EquipmentType.Armor;
        }
    }
}