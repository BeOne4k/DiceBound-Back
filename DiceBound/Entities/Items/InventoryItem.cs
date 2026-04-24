using DiceBound.Common;
using DiceBound.Entity_s.Characters;

namespace DiceBound.Entity_s.Items
{
    public class InventoryItem : BaseEntity
    {
        public Guid CharacterId { get; set; }
        public Character Character { get; set; } = null!;

        public Guid ItemId { get; set; }
        public Item Item { get; set; } = null!;

        public bool IsEquipped { get; set; }
        public int Quantity { get; set; } = 1;


    }

}
