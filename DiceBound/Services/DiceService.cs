namespace DiceBound.Services
{
    public class DiceService
    {
        private readonly Random _random = new();

        public int Roll(int sides)
            => _random.Next(1, sides + 1);

        public int Roll(int count, int sides)
        {
            int total = 0;
            for (int i = 0; i < count; i++)
                total += Roll(sides);

            return total;
        }

        public int RollD20(int modifier = 0)
            => Roll(20) + modifier;

        public int RollAttack(int attackBonus)
        {
            return Roll(20) + attackBonus;
        }
    }
}
