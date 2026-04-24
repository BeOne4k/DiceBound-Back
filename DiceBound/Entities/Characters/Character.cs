using DiceBound.Common;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Entity_s.Identity;
using DiceBound.Entity_s.Items;
using System.Diagnostics;

namespace DiceBound.Entity_s.Characters
{
    public class Character : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid RaceId { get; set; }
        public Race Race { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }

        public int AttackBonus { get; set; } = 0;
        public int BaseAttack { get; set; } = 0;

        public int HP { get; set; } = 100;
        public int ArmorClass { get; set; } = 14;

        public ICollection<InventoryItem> Inventory { get; set; } = new List<InventoryItem>();
        public ICollection<CombatLog> CombatLogs { get; set; } = new List<CombatLog>();
    }

}
