using DiceBound.Common;

namespace DiceBound.Entity_s.Gameplay
{
    public class Mission : BaseEntity
    {
        public string Name { get; set; } = null!;

        public int MinLevel { get; set; }
        public int Difficulty { get; set; }

        public int RewardExperience { get; set; }
        public Guid? BossId { get; set; }
        public Boss? Boss { get; set; }

        public ICollection<Enemy> Enemies { get; set; } = new List<Enemy>();
        public ICollection<CombatLog> CombatLogs { get; set; } = new List<CombatLog>();
    }

}
