
using UnityEngine;

namespace ProtoWorld
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Items/Weapon")]
    public class WeaponData : EquipmentData
    {
        public float atk;

        public WeaponData()
        {
            equipmentType = EquipmentType.Weapon;
        }
    }
}