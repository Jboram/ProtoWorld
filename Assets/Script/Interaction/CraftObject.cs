namespace ProtoWorld
{
    public class CraftObject : IInteractable
    {
        public override string GetInteractionText()
        {
            return "제작 하기";
        }

        public override void Interact(PlayerController playerController)
        {
            UIManager.inst.OpenWindow<CraftWindow>();
            var inventoryWindow = UIManager.inst.GetWindow<InventoryWindow>();
            inventoryWindow.Hide();
            inventoryWindow.Show();
            inventoryWindow.SetInventory(playerController.Inventory);
        }
    }
}