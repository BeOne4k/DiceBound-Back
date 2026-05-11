namespace DiceBound.DTOs.Mission
{
    public class AddMissionRewardDto
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
