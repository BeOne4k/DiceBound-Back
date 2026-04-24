using DiceBound.Entity_s.Characters;

public class LevelService
{

    private const int MaxLevel = 20;
    private const int MaxAttackBonus = 8;

    public int GetXpForNextLevel(int level)
    {
        return 100 * (int)Math.Pow(2, level - 1);
    }

    public int GetTotalXpForLevel(int level)
    {
        int total = 0;

        for (int i = 1; i < level; i++)
        {
            total += GetXpForNextLevel(i);
        }

        return total;
    }

    public void ApplyLevelUp(Character character)
    {
        while (character.Level < MaxLevel)
        {
            int xpNeeded = GetXpForNextLevel(character.Level);

            if (character.Experience < xpNeeded)
                break;

            character.Experience -= xpNeeded;
            character.Level++;

            character.HP += 10;
            character.ArmorClass += 1;

            if (character.AttackBonus < MaxAttackBonus)
            {
                character.AttackBonus += 1;
            }
            else
            {
                character.BaseAttack += 1;
            }
        }
    }
}