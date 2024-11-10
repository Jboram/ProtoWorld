using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProtoWorld
{
    public class CraftSlotUI : BaseUI, IPointerClickHandler, IDropHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image unknownIcon;
        [SerializeField] private TextMeshProUGUI amount;

        private ItemData itemData;
        public ItemData ItemData => itemData;
        private bool isResult;

        public Action OnUpdateItem;

        public void SetItemData(ItemData itemData)
        {
            SetUnknownItem(false);

            this.itemData = itemData;
            icon.sprite = itemData.icon;
            OnUpdateItem?.Invoke(); // null이면 실행 안하고, null이 아니면 이벤트 뿌린다.
        }

        public void SetUnknownItem(bool isUnknown)
        {
            icon.enabled = !isUnknown;
            unknownIcon.enabled = isUnknown;
        }

        public void SetItemAmount(int amount)
        {
            this.amount.text = amount.ToString();
        }

        public void SetResultSlot(bool isResult)
        {
            this.isResult = isResult;
        }

        public void RemoveItem()
        {
            SetUnknownItem(false);
            itemData = null;
            icon.sprite = null;
            amount.text = string.Empty;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!isResult)
                {
                    RemoveItem();
                    OnUpdateItem?.Invoke();
                }
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            InvenSlotUI invenSlotUI = eventData.pointerDrag?.GetComponent<InvenSlotUI>();
            if (invenSlotUI != null)
            {
                if (isResult == false)
                {
                    SetItemData(invenSlotUI.Item.ItemData);
                }
            }
        }

        public bool IsExistItem()
        {
            return itemData != null;
        }
    }

}