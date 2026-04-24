using DiceBound.DTOs.Race;

namespace DiceBound.Interfaces
{
    public interface IRaceService
    {
        Task<IEnumerable<RaceDto>> GetAllAsync();
        Task<RaceDto?> GetByIdAsync(Guid id);
        Task<RaceDto> CreateAsync(CreateRaceDto dto);
        Task<RaceDto?> UpdateAsync(UpdateRaceDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
