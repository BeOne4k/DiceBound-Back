using DiceBound.Common;
using DiceBound.Entities.Enums;

namespace DiceBound.Entity_s.Items
{
    public class Item : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ItemType Type { get; set; }

        public int DiceCount { get; set; }
        public int DiceSides { get; set; }
        public int Modifier { get; set; }
        public ItemRarity Rarity { get; set; } = ItemRarity.Common;
        public string Img { get; set; }


        public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    }

}
