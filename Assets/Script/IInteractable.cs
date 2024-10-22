using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld
{
    public abstract class IInteractable : MonoBehaviour 
    {
        public abstract void Interact();

        public float interactionDistance = 3f;

        // 상호작용 조건을 확인하는 메서드
        public virtual bool CanInteract(Vector3 playerPosition)
        {
            // 플레이어와의 거리를 확인하여 상호작용 가능 여부 반환
            float distance = Vector3.Distance(playerPosition, transform.position);
            return distance <= interactionDistance;
        }
    }
}
