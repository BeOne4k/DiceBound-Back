namespace DiceBound.DTOs.Character
{
    public class CreateCharacterDto
    {
        public Guid UserId { get; set; }
        public Guid RaceId { get; set; }

        public string Name { get; set; } = null!;
    }
}
