using AutoMapper;
using DiceBound.DTOs.Mission;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Interfaces;

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
        var missions = await _unitOfWork.Repository<Mission>().GetAllAsync();
        return _mapper.Map<IEnumerable<MissionDto>>(missions);
    }

    public async Task<MissionDto?> GetByIdAsync(Guid id)
    {
        var mission = await _unitOfWork.Repository<Mission>().GetByIdAsync(id);
        return mission == null ? null : _mapper.Map<MissionDto>(mission);
    }

    public async Task<MissionDto> CreateAsync(CreateMissionDto dto)
    {
        var mission = _mapper.Map<Mission>(dto);

        await _unitOfWork.Repository<Mission>().AddAsync(mission);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<MissionDto>(mission);
    }

    public async Task<MissionDto?> UpdateAsync(UpdateMissionDto dto)
    {
        var mission = await _unitOfWork.Repository<Mission>().GetByIdAsync(dto.Id);
        if (mission == null) return null;

        _mapper.Map(dto, mission);

        _unitOfWork.Repository<Mission>().Update(mission);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<MissionDto>(mission);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var mission = await _unitOfWork.Repository<Mission>().GetByIdAsync(id);
        if (mission == null) return false;

        _unitOfWork.Repository<Mission>().Delete(mission);
        await _unitOfWork.SaveAsync();

        return true;
    }
}