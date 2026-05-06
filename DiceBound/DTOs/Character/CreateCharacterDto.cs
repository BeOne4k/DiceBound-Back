namespace DiceBound.DTOs.Character
{
    public class CreateCharacterDto
    {
        public string Name { get; set; } = null!;
        public Guid RaceId { get; set; }
        public Guid UserId { get; set; }
    }
}