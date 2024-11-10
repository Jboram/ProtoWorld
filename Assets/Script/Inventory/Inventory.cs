using System;

namespace ProtoWorld
{
    public class Inventory
    {
        // 인벤에 들어있는 아이템들
        private Item[] items;
        public Item[] Items => items;
        public int Length => items.Length;
        public readonly int MaxCount = 24;
        private int itemCount = 0;

        public event Action<Inventory> OnInventoryUpdated;

        public Inventory()
        {
            items = new Item[MaxCount];
        }

        public void Add(ItemData itemData, int amount)
        {
            var item = FindItem(itemData);
            if (item == null)
            {
                int index = FindEmptySlot();
                if (index != -1)
                {
                    items[index] = new Item(itemData, amount);
                    itemCount++;
                }
                else
                {
                    // 인벤 꽉참
                }
            }
            else
            {
                item.Add(amount);
            }

            OnInventoryUpdated?.Invoke(this);
        }

        public void Remove(ItemData itemData, int amount)
        {
            var index = FindSlot(itemData);
            if (index == -1)
            {
                return;
            }
            var item = items[index];
            item.Add(-amount);
            if (item.Empty())
            {
                 for (int i = index; i < itemCount - 1; i++)
                {
                    items[i] = items[i + 1];
                }
                items[itemCount - 1] = null;
                itemCount--;
            }

            OnInventoryUpdated?.Invoke(this);
        }

        public Item FindItem(ItemData itemData)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (item == null)
                {
                    continue;
                }

                if (item.ItemData.code == itemData.code)
                {
                    return item;
                }
            }

            return null;
        }

        public int FindEmptySlot()
        {
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (item == null)
                {
                    return i;
                }
            }

            return -1;
        }

        public int FindSlot(ItemData itemData)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                if (item == null)
                {
                    continue;
                }

                if (item.ItemData.code == itemData.code)
                {
                    return i;
                }
            }

            return -1;
        }
    }

}