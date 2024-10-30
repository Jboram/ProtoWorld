using ProtoWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld
{
    public class Collectible : IInteractable
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private int amount;

        public override string GetInteractionText()
        {
            return "채집하기";
        }

        public override void Interact(PlayerController playerController)
        {
            playerController.Inventory.Add(itemData, amount);
            Collect();
        }

        private void Collect()
        {
            Destroy(gameObject);
        }
    }
}