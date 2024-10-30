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
    }
}