namespace DiceBound.DTOs.Mission
{
    public class MissionRewardItemDto
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemType { get; set; } = null!;
        public string ItemRarity { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
