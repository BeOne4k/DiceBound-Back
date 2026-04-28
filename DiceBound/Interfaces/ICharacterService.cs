using DiceBound.DTOs.Character;

public interface ICharacterService
{
    Task<IEnumerable<CharacterDto>> GetAllAsync();
    Task<CharacterDto?> GetByIdAsync(Guid id);
    Task<CharacterDto> CreateAsync(CreateCharacterDto dto);
    Task<CharacterDto?> UpdateAsync(UpdateCharacterDto dto);
    Task<bool> DeleteAsync(Guid id);
    // Исправлено: было string, стало Guid
    Task<List<CharacterDto>> GetByUserIdAsync(Guid userId);
}