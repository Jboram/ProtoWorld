using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoWorld
{
    public class InvenSlotUI : BaseUI
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI stackText;

        private Item item;
        public void UpdateItem(Item item)
        {
            this.item = item;

            if (item == null )
            {
                icon.sprite = null;
                stackText.text = string.Empty;
            }
            else
            {
                icon.sprite = item.ItemData.icon;
                stackText.text = item.Amount.ToString();
            }
        }
    }

}