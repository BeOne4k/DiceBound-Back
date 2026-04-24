namespace DiceBound.DTOs.Character
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public int Level { get; set; }
        public int Experience { get; set; }

        public string RaceName { get; set; } = null!;
    }
}
