namespace DiceBound.DTOs.Race
{
    public class UpdateRaceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public int BaseStrength { get; set; }
        public int BaseDexterity { get; set; }
        public int BaseConstitution { get; set; }
        public int BaseIntelligence { get; set; }
    }
}
