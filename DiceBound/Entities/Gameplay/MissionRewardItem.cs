using DiceBound.Common;
using DiceBound.Entity_s.Items;

namespace DiceBound.Entity_s.Gameplay
{
    public class MissionRewardItem : BaseEntity
    {
        public Guid MissionId { get; set; }
        public Mission Mission { get; set; } = null!;

        public Guid ItemId { get; set; }
        public Item Item { get; set; } = null!;

        public int Quantity { get; set; } = 1;
    }
}
