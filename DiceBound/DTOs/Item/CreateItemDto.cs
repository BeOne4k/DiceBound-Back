namespace DiceBound.DTOs.Item
{
    public class CreateItemDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Rarity { get; set; } = null!;

        public int DiceCount { get; set; }
        public int DiceSides { get; set; }
        public int Modifier { get; set; }

        public string Img { get; set; } = null!;
    }
}
