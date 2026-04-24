namespace DiceBound.DTOs.Mission
{
    public class CreateMissionDto
    {
        public string Name { get; set; } = null!;

        public int MinLevel { get; set; }
        public int Difficulty { get; set; }

        public int RewardExperience { get; set; }

        public Guid? BossId { get; set; }
    }
}
