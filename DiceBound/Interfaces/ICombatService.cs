using DiceBound.DTOs.Combat;

namespace DiceBound.Interfaces
{
    public interface ICombatService
    {
        Task<CombatResultDto> StartCombat(StartCombatDto dto);
    }
}
