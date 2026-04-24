public class CreateBossDto
{
    public string Name { get; set; } = null!;

    public int RequiredLevel { get; set; }
    public int HP { get; set; }
    public int ArmorClass { get; set; }

    public int XpValue { get; set; }
}