namespace DiceBound.DTOs.Mission
{
    public class UpdateMissionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int MinLevel { get; set; }
        public int Difficulty { get; set; }

        public int RewardExperience { get; set; }

        public Guid? BossId { get; set; }
    }
}
