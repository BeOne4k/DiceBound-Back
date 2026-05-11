namespace DiceBound.DTOs.Item
{
    public class InventoryItemDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Rarity { get; set; } = null!;
        public int DiceCount { get; set; }
        public int DiceSides { get; set; }
        public int Modifier { get; set; }
        public bool IsEquipped { get; set; }
        public int Quantity { get; set; }
    }
}
