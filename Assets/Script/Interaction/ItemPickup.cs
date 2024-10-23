using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld 
{
public class ItemPickup : IInteractable
{
       public string itemName = "Unknown Item";  // 아이템 이름

        public override string GetInteractionText() {
            return "줍기";
        }

        // 상호작용 메서드 구현
        public override void Interact()
        {
            PickupItem();
        }

        private void PickupItem()
        {
            Debug.Log(itemName + " 아이템을 획득.");
            // 아이템 줍기 처리 로직 (인벤토리에 추가 등)
            Destroy(gameObject);  // 아이템을 줍고 오브젝트 삭제
        }
}

}
