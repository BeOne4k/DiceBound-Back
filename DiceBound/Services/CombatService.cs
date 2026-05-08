using DiceBound.DTOs.Combat;
using DiceBound.Entity_s.Characters;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Interfaces;

namespace DiceBound.Services
{
    public class CombatService : ICombatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DiceService _dice;
        private readonly LevelService _levelService;

        public CombatService(IUnitOfWork unitOfWork, DiceService dice, LevelService levelService)
        {
            _unitOfWork = unitOfWork;
            _dice = dice;
            _levelService = levelService;
        }

        public async Task<CombatResultDto> StartCombat(StartCombatDto dto)
        {
            var result = new CombatResultDto();

            var character = await _unitOfWork.Repository<Character>()
                .GetByIdAsync(dto.CharacterId);

            var mission = await _unitOfWork.Repository<Mission>()
                .GetByIdAsync(dto.MissionId);

            if (character == null || mission == null)
                throw new Exception("Invalid data");

            var enemies = await _unitOfWork.Repository<Enemy>().GetAllAsync();
            var missionEnemies = enemies.Where(e => e.MissionId == mission.Id).ToList();

            // mission.Boss не подгружается через FindAsync — загружаем явно
            Boss? boss = null;
            if (mission.BossId.HasValue)
                boss = await _unitOfWork.Repository<Boss>().GetByIdAsync(mission.BossId.Value);

            int totalXp = 0;

            foreach (var enemy in missionEnemies)
            {
                if (!Fight(character, enemy, result.Logs))
                {
                    result.IsWin = false;
                    return result;
                }

                totalXp += enemy.XpValue;
            }

            if (boss != null)
            {
                if (!Fight(character, boss, result.Logs))
                {
                    result.IsWin = false;
                    return result;
                }

                totalXp += boss.XpValue;
            }

            totalXp += mission.RewardExperience;

            int oldLevel = character.Level;

            character.Experience += totalXp;

            _levelService.ApplyLevelUp(character);

            if (character.Level > oldLevel)
            {
                result.Logs.Add($"LEVEL UP! {oldLevel} → {character.Level}");
            }

            await _unitOfWork.SaveAsync();

            result.IsWin = true;
            result.GainedXp = totalXp;

            return result;
        }

        private bool Fight(dynamic character, dynamic enemy, List<string> logs)
        {
            int charHP = character.HP;
            int enemyHP = enemy.HP;

            logs.Add($" Fight vs {enemy.Name} (AC:{enemy.ArmorClass} HP:{enemyHP}) | Your AC:{character.ArmorClass} HP:{charHP}");

            while (charHP > 0 && enemyHP > 0)
            {
                int roll = _dice.RollAttack(character.AttackBonus + character.BaseAttack);

                if (roll == 20)
                {
                    int dmg = _dice.Roll(2, 8);
                    enemyHP -= dmg;
                    logs.Add($" CRIT! {dmg} (roll:20)");
                }
                else if (roll >= enemy.ArmorClass)
                {
                    int dmg = _dice.Roll(1, 8);
                    enemyHP -= dmg;
                    logs.Add($"Hit {dmg} (roll:{roll} vs AC:{enemy.ArmorClass})");
                }
                else
                {
                    logs.Add($"Miss (roll:{roll} vs AC:{enemy.ArmorClass})");
                }

                if (enemyHP <= 0) break;

                int enemyRoll = _dice.Roll(20);

                if (enemyRoll >= character.ArmorClass)
                {
                    int dmg = _dice.Roll(1, 6);
                    charHP -= dmg;
                    logs.Add($"Enemy hit {dmg} (roll:{enemyRoll} vs your AC:{character.ArmorClass})");
                }
                else
                {
                    logs.Add($"Enemy miss (roll:{enemyRoll} vs your AC:{character.ArmorClass})");
                }
            }

            if (charHP > 0)
            {
                logs.Add($" {enemy.Name} defeated (HP left: {charHP})");
                return true;
            }

            logs.Add("❌ You died");
            return false;
        }
    }
}