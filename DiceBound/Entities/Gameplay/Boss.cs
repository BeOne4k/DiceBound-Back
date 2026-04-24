using DiceBound.Common;

namespace DiceBound.Entity_s.Gameplay
{
    public class Boss : BaseEntity
    {
        public string Name { get; set; } = null!;

        public int RequiredLevel { get; set; }
        public int HP { get; set; }
        public int ArmorClass { get; set; }

        public int XpValue { get; set; }

        public ICollection<CombatLog> CombatLogs { get; set; } = new List<CombatLog>();
    }

}
