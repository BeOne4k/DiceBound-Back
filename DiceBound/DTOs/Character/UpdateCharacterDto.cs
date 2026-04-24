public class UpdateCharacterDto
{
    public Guid Id { get; set; }  

    public string Name { get; set; } = null!;

    public Guid RaceId { get; set; }

    public int Level { get; set; }
    public int Experience { get; set; }
}