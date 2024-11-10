using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoWorld 
{
    public class CraftWindow : WindowBase
    {
        [SerializeField] private CraftSlotUI ingredientSlot1;
        [SerializeField] private CraftSlotUI ingredientSlot2;
        [SerializeField] private CraftSlotUI resultSlot;
        [SerializeField] private TextMeshProUGUI resultName;
        [SerializeField] private Button btnCraft;
        [SerializeField] private Button btnClose;

        private Recipe recipe;
        protected override void Awake()
        {
            base.Awake();

            resultSlot.SetResultSlot(true);

            ingredientSlot1.OnUpdateItem += OnUpdateItem;
            ingredientSlot2.OnUpdateItem += OnUpdateItem;

            btnClose.onClick.AddListener(Hide);
            btnCraft.onClick.AddListener(OnClickCraft);
        }

        protected override void OnHide()
        {
            base.OnHide();
            UIManager.inst.CloseWindow<InventoryWindow>();
        }

        private void OnClickCraft()
        {
            if (recipe == null)
            {
                return;
            }
            var controller = PlayerService.inst.Controller;
            var Inventory = controller.Inventory;
            var item1 = Inventory.FindItem(recipe.ingredient1);
            var item2 = Inventory.FindItem(recipe.ingredient2);

            var resultItem = CraftManager.inst.Craft(item1, item2);
            if (resultItem != null)
            {
                int amount1 = recipe.amount1;
                int amount2 = recipe.amount2;
                if (item1.Amount == amount1)
                {
                    ingredientSlot1.RemoveItem();
                }
                if (item2.Amount == amount2)
                {
                    ingredientSlot2.RemoveItem();
                }

                Inventory.Remove(item1.ItemData, amount1);
                Inventory.Remove(item2.ItemData, amount2);
                Inventory.Add(resultItem.ItemData, resultItem.Amount);
                OnUpdateItem();
            }
        }

        private void OnUpdateItem()
        {
            if (ingredientSlot1.IsExistItem() && ingredientSlot2.IsExistItem())
            {
                recipe = CraftManager.inst.GetResultRecipe(ingredientSlot1.ItemData, ingredientSlot2.ItemData);
                if (recipe != null)
                {
                    if(CraftManager.inst.IsMade(recipe.result))
                    {
                        resultSlot.SetItemData(recipe.result);
                        resultSlot.SetItemAmount(recipe.amount);
                        resultName.text = recipe.result.itemName;
                    }
                    else
                    {
                        resultSlot.SetUnknownItem(true);
                        resultName.text = "???";
                    }
                }
            }
            else
            {
                recipe = null;
                resultSlot.RemoveItem();
                resultName.text = string.Empty;
            }
        }
    }
}