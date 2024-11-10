using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProtoWorld
{
    [RequireComponent(typeof(CanvasGroup))]
    public class InvenSlotUI : BaseUI, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI stackText;

        private Item item;
        public Item Item => item;

        private Canvas canvas;
        private CanvasGroup canvasGroup;

        private GameObject dragIcon; // 드래그할 때 생성되는 복제 아이템

        private Vector2 originalPosition; // 원래 위치 저장

        protected override void Awake()
        {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>(); // 드래그에 사용할 캔버스 참조
        }

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
        public void OnBeginDrag(PointerEventData eventData)
        {
            dragIcon = new GameObject("DragIcon", typeof(Image));
            dragIcon.transform.SetParent(canvas.transform, false);

            Image dragImage = dragIcon.GetComponent<Image>();
            dragImage.sprite = icon.sprite;
            dragImage.raycastTarget = false;

            RectTransform dragIconRect = dragIcon.GetComponent<RectTransform>();
            dragIconRect.sizeDelta = GetComponent<RectTransform>().sizeDelta;
            dragIconRect.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragIcon != null)
            {
                dragIcon.GetComponent<RectTransform>().position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // 드래그가 끝나면 복제본 삭제
            if (dragIcon != null)
            {
                Destroy(dragIcon);
            }
        }
    }

}