namespace ProtoWorld
{
    public class Item
    {
        private ItemData itemData;
        public ItemData ItemData => itemData;
        private int amount;
        public int Amount => amount;

        public Item(ItemData itemData, int amount)
        {
            this.itemData = itemData;
            this.amount = amount;
        }

        public void Add(int amount)
        {
            this.amount += amount;
        }

        public bool Empty()
        {
            return amount <= 0;
        }
    }
}