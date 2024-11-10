using UnityEngine;

namespace ProtoWorld
{
    public enum ItemType
    {
        Consumable,
        Equipment,
        Misc,
    }

    [System.Serializable]
    public abstract class ItemData : ScriptableObject
    {
        public int code;
        public ItemType itemType;
        public string itemName;
        public string itemDesc;
        public int maxStackSize = 1;
        public Sprite icon;

        public override bool Equals(object obj)
        {
            if (obj is ItemData itemData)
            {
                return code == itemData.code;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return code.GetHashCode();
        }
    }
}