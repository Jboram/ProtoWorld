using ProtoWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld
{
    public class Collectible : IInteractable
    {
        public string itemName = "채집물";
        public override string GetInteractionText()
        {
            return "채집하기";
        }

        public override void Interact()
        {
            Collect();
        }

        private void Collect()
        {
            Debug.Log(itemName + " 아이템을 획득.");
            Destroy(gameObject);
        }
    }
}