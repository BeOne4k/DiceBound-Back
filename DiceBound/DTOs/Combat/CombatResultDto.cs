namespace DiceBound.DTOs.Combat
{
    public class CombatResultDto
    {
        public bool IsWin { get; set; }

        public int GainedXp { get; set; }

        public List<string> Logs { get; set; } = new();
    }
}
