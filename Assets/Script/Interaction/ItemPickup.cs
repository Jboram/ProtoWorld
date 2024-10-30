using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld 
{
public class ItemPickup : IInteractable
{
        [SerializeField] private ItemData itemData;
        [SerializeField] private int amount;

        public override string GetInteractionText() {
            return "줍기";
        }

        // 상호작용 메서드 구현
        public override void Interact(PlayerController playerController)
        {
            playerController.Inventory.Add(itemData, amount);
            PickupItem();
        }

        private void PickupItem()
        {
            // 아이템 줍기 처리 로직 (인벤토리에 추가 등)
            Destroy(gameObject);  // 아이템을 줍고 오브젝트 삭제
        }
}

}
