using DiceBound.Common;
using DiceBound.Entity_s.Gameplay;

public class Enemy : BaseEntity
{
    public string Name { get; set; } = null!;

    public int HP { get; set; }
    public int ArmorClass { get; set; }

    public int XpValue { get; set; }

    public Guid MissionId { get; set; }
    public Mission Mission { get; set; } = null!;
}