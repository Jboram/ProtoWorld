using System.Collections.Generic;
using System.Linq;

namespace ProtoWorld 
{
    public class InventoryWindow : WindowBase
    {
        private List<InvenSlotUI> invenSlotList;
        private Inventory inventory;

        protected override void Awake()
        {
            base.Awake();
            invenSlotList = transform.Find("Frame").GetComponentsInChildren<InvenSlotUI>().ToList();
        }

        protected override void OnShow()
        {
            base.OnShow();

        }
        protected override void OnHide()
        {
            base.OnHide();
            if(inventory != null)
            {
                inventory.OnInventoryUpdated -= UpdateInventory;
                inventory = null;
            }
        }

        public void SetInventory(Inventory inventory)
        {
            this.inventory = inventory;
            inventory.OnInventoryUpdated += UpdateInventory;
            UpdateInventory(inventory);
        }

        public void UpdateInventory(Inventory inventory)
        {
            var items = inventory.Items;
            for (int i = 0; i < invenSlotList.Count; ++i)
            {
                if (i < items.Length)
                {
                    invenSlotList[i].UpdateItem(items[i]);
                }
            }
        }
    }
}