namespace _Project.Inventory.Structures
{
    public struct ReplaceData
    {
        public ItemSOProperty ItemData;
        public uint Amount;

        public ReplaceData(ItemSOProperty itemData, uint amount)
        {
            ItemData = itemData;
            Amount = amount;
        }
    }
}