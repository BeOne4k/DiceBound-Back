using DiceBound.DTOs.Enemy;

namespace DiceBound.Interfaces
{
    public interface IEnemyService
    {
        Task<IEnumerable<EnemyDto>> GetAllAsync();
        Task<EnemyDto?> GetByIdAsync(Guid id);
        Task<EnemyDto> CreateAsync(CreateEnemyDto dto);
        Task<EnemyDto?> UpdateAsync(UpdateEnemyDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
