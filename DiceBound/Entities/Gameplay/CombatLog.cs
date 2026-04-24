using DiceBound.Common;
using DiceBound.Entity_s.Characters;

namespace DiceBound.Entity_s.Gameplay
{
    public class CombatLog : BaseEntity
    {
        public Guid CharacterId { get; set; }
        public Character Character { get; set; } = null!;

        public Guid MissionId { get; set; }
        public Mission Mission { get; set; } = null!;

        public Guid? BossId { get; set; }
        public Boss? Boss { get; set; }

        public int TotalRoll { get; set; }
        public bool Success { get; set; }
    }

}
