namespace DiceBound.DTOs.Character
{
    public class CreateCharacterDto
    {
        public string Name { get; set; } = null!;
        public Guid RaceId { get; set; }
        // UserId устанавливается контроллером из JWT-токена, не из тела запроса
        public Guid UserId { get; set; }
    }
}