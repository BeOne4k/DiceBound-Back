using AutoMapper;
using DiceBound.DTOs.Mission;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Interfaces;
using Microsoft.EntityFrameworkCore;

public class MissionService : IMissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MissionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MissionDto>> GetAllAsync()
    {
        var missions = await _unitOfWork.Repository<Mission>()
            .Query()
            .Include(m => m.Boss)
            .Include(m => m.RewardItems)
                .ThenInclude(r => r.Item)
            .ToListAsync();

        return missions.Select(MapToDto);
    }

    public async Task<MissionDto?> GetByIdAsync(Guid id)
    {
        var mission = await _unitOfWork.Repository<Mission>()
            .Query()
            .Include(m => m.Boss)
            .Include(m => m.RewardItems)
                .ThenInclude(r => r.Item)
            .FirstOrDefaultAsync(m => m.Id == id);

        return mission == null ? null : MapToDto(mission);
    }

    public async Task<MissionDto> CreateAsync(CreateMissionDto dto)
    {
        var mission = _mapper.Map<Mission>(dto);
        await _unitOfWork.Repository<Mission>().AddAsync(mission);
        await _unitOfWork.SaveAsync();
        return MapToDto(mission);
    }

    public async Task<MissionDto?> UpdateAsync(UpdateMissionDto dto)
    {
        var mission = await _unitOfWork.Repository<Mission>().GetByIdAsync(dto.Id);
        if (mission == null) return null;
        _mapper.Map(dto, mission);
        _unitOfWork.Repository<Mission>().Update(mission);
        await _unitOfWork.SaveAsync();
        return MapToDto(mission);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var mission = await _unitOfWork.Repository<Mission>().GetByIdAsync(id);
        if (mission == null) return false;
        _unitOfWork.Repository<Mission>().Delete(mission);
        await _unitOfWork.SaveAsync();
        return true;
    }

    private static MissionDto MapToDto(Mission m) => new MissionDto
    {
        Id               = m.Id,
        Name             = m.Name,
        MinLevel         = m.MinLevel,
        Difficulty       = m.Difficulty,
        RewardExperience = m.RewardExperience,
        BossId           = m.BossId,
        BossName         = m.Boss?.Name,
        RewardItems      = m.RewardItems.Select(r => new MissionRewardItemDto
        {
            Id         = r.Id,
            ItemId     = r.ItemId,
            ItemName   = r.Item?.Name ?? "",
            ItemType   = r.Item?.Type.ToString() ?? "",
            ItemRarity = r.Item?.Rarity.ToString() ?? "",
            Quantity   = r.Quantity
        }).ToList()
    };
}
