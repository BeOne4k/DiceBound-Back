namespace DiceBound.Services
{
    using AutoMapper;
    using DiceBound.DTOs.Enemy;
    using DiceBound.Entity_s.Gameplay;
    using DiceBound.Interfaces;

    public class EnemyService : IEnemyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnemyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnemyDto>> GetAllAsync()
        {
            var enemies = await _unitOfWork.Repository<Enemy>().GetAllAsync();
            return _mapper.Map<IEnumerable<EnemyDto>>(enemies);
        }

        public async Task<EnemyDto?> GetByIdAsync(Guid id)
        {
            var enemy = await _unitOfWork.Repository<Enemy>().GetByIdAsync(id);
            return enemy == null ? null : _mapper.Map<EnemyDto>(enemy);
        }

        public async Task<EnemyDto> CreateAsync(CreateEnemyDto dto)
        {
            var enemy = _mapper.Map<Enemy>(dto);

            await _unitOfWork.Repository<Enemy>().AddAsync(enemy);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<EnemyDto>(enemy);
        }

        public async Task<EnemyDto?> UpdateAsync(UpdateEnemyDto dto)
        {
            var enemy = await _unitOfWork.Repository<Enemy>().GetByIdAsync(dto.Id);
            if (enemy == null) return null;

            _mapper.Map(dto, enemy);

            _unitOfWork.Repository<Enemy>().Update(enemy);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<EnemyDto>(enemy);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var enemy = await _unitOfWork.Repository<Enemy>().GetByIdAsync(id);
            if (enemy == null) return false;

            _unitOfWork.Repository<Enemy>().Delete(enemy);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}
