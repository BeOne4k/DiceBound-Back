
namespace DiceBound.DTOs.Enemy
{
    public class EnemyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int HP { get; set; }
        public int ArmorClass { get; set; }

        public int XpValue { get; set; }

        public Guid MissionId { get; set; }
    }
}
