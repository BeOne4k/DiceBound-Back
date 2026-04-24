using AutoMapper;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Interfaces;

public class BossService : IBossService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BossService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BossDto>> GetAllAsync()
    {
        var bosses = await _unitOfWork.Repository<Boss>().GetAllAsync();
        return _mapper.Map<IEnumerable<BossDto>>(bosses);
    }

    public async Task<BossDto?> GetByIdAsync(Guid id)
    {
        var boss = await _unitOfWork.Repository<Boss>().GetByIdAsync(id);
        return boss == null ? null : _mapper.Map<BossDto>(boss);
    }

    public async Task<BossDto> CreateAsync(CreateBossDto dto)
    {
        var boss = _mapper.Map<Boss>(dto);

        await _unitOfWork.Repository<Boss>().AddAsync(boss);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<BossDto>(boss);
    }

    public async Task<BossDto> UpdateAsync(UpdateBossDto dto)
    {
        var boss = await _unitOfWork.Repository<Boss>().GetByIdAsync(dto.Id);
        if (boss == null) return null;

        _mapper.Map(dto, boss);

        _unitOfWork.Repository<Boss>().Update(boss);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<BossDto>(boss);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var boss = await _unitOfWork.Repository<Boss>().GetByIdAsync(id);
        if (boss == null) return false;

        _unitOfWork.Repository<Boss>().Delete(boss);
        await _unitOfWork.SaveAsync();

        return true;
    }
}