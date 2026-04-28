using AutoMapper;
using DiceBound.DTOs.Character;
using DiceBound.Entity_s.Characters;
using DiceBound.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CharacterService : ICharacterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CharacterService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IEnumerable<CharacterDto>> GetAllAsync()
    {
        var characters = await _unitOfWork.Repository<Character>().GetAllAsync();
        return _mapper.Map<IEnumerable<CharacterDto>>(characters);
    }

    public async Task<CharacterDto?> GetByIdAsync(Guid id)
    {
        var character = await _unitOfWork.Repository<Character>().GetByIdAsync(id);
        return character == null ? null : _mapper.Map<CharacterDto>(character);
    }

    public async Task<CharacterDto> CreateAsync(CreateCharacterDto dto)
    {
        var race = await _unitOfWork.Repository<Race>().GetByIdAsync(dto.RaceId);

        if (race == null)
            throw new Exception("Race not found");

        var character = new Character
        {
            Name = dto.Name,
            UserId = dto.UserId,
            RaceId = dto.RaceId,

            Strength = race.BaseStrength,
            Dexterity = race.BaseDexterity,
            Constitution = race.BaseConstitution,
            Intelligence = race.BaseIntelligence,

            HP = 100 + race.BaseConstitution * 2,
            ArmorClass = 10 + race.BaseDexterity
        };

        await _unitOfWork.Repository<Character>().AddAsync(character);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<CharacterDto>(character);
    }


    public async Task<CharacterDto?> UpdateAsync(UpdateCharacterDto dto)
    {
        var character = await _unitOfWork.Repository<Character>().GetByIdAsync(dto.Id);
        if (character == null) return null;

        _mapper.Map(dto, character);

        _unitOfWork.Repository<Character>().Update(character);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<CharacterDto>(character);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var character = await _unitOfWork.Repository<Character>().GetByIdAsync(id);
        if (character == null) return false;

        _unitOfWork.Repository<Character>().Delete(character);
        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<List<CharacterDto>> GetByUserIdAsync(Guid userId)
    {
        var characters = await _unitOfWork
            .Repository<Character>()
            .FindAsync(x => x.UserId == userId);

        return _mapper.Map<List<CharacterDto>>(characters);
    }
}