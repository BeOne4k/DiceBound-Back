using AutoMapper;
using DiceBound.DTOs.Race;
using DiceBound.Entity_s.Characters;
using DiceBound.Interfaces;

namespace DiceBound.Services
{
    public class RaceService : IRaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RaceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RaceDto>> GetAllAsync()
        {
            var races = await _unitOfWork.Repository<Race>().GetAllAsync();
            return _mapper.Map<IEnumerable<RaceDto>>(races);
        }

        public async Task<RaceDto?> GetByIdAsync(Guid id)
        {
            var race = await _unitOfWork.Repository<Race>().GetByIdAsync(id);
            return race == null ? null : _mapper.Map<RaceDto>(race);
        }

        public async Task<RaceDto> CreateAsync(CreateRaceDto dto)
        {
            var race = _mapper.Map<Race>(dto);

            await _unitOfWork.Repository<Race>().AddAsync(race);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RaceDto>(race);
        }

        public async Task<RaceDto?> UpdateAsync(UpdateRaceDto dto)
        {
            var race = await _unitOfWork.Repository<Race>().GetByIdAsync(dto.Id);
            if (race == null) return null;

            _mapper.Map(dto, race);

            _unitOfWork.Repository<Race>().Update(race);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RaceDto>(race);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var race = await _unitOfWork.Repository<Race>().GetByIdAsync(id);
            if (race == null) return false;

            _unitOfWork.Repository<Race>().Delete(race);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}