public interface IBossService
{
    Task<IEnumerable<BossDto>> GetAllAsync();
    Task<BossDto?> GetByIdAsync(Guid id);
    Task<BossDto> CreateAsync(CreateBossDto dto);
    Task<BossDto?> UpdateAsync(UpdateBossDto dto);
    Task<bool> DeleteAsync(Guid id);
}