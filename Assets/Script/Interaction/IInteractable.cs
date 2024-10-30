using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld
{
     public enum InteractableType
    {
        None,
        NPC,
        Collectible,
        Pickable,
    }

    public abstract class IInteractable : MonoBehaviour 
    {
        private InteractableType type;
        public InteractableType Type => type;
        protected float interactionDistance = 2f;
        public abstract string GetInteractionText();
        public abstract void Interact(PlayerController playerController);


        // 상호작용 조건을 확인하는 메서드
        public virtual bool CanInteract(Vector3 playerPosition)
        {
            // 플레이어와의 거리를 확인하여 상호작용 가능 여부 반환
            return Distance(playerPosition) <= interactionDistance;
        }

        public float Distance(Vector3 playerPosition)
        {
            return Vector3.Distance(playerPosition, transform.position);
        }
    }
}
