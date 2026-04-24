using DiceBound.DTOs.Mission;

public interface IMissionService
{
    Task<IEnumerable<MissionDto>> GetAllAsync();
    Task<MissionDto?> GetByIdAsync(Guid id);
    Task<MissionDto> CreateAsync(CreateMissionDto dto);
    Task<MissionDto?> UpdateAsync(UpdateMissionDto dto);
    Task<bool> DeleteAsync(Guid id);
}