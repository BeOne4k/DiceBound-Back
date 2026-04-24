using AutoMapper;
using DiceBound.DTOs.Item;
using DiceBound.Entity_s.Items;
using DiceBound.Interfaces;

namespace DiceBound.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Repository<Item>().GetAllAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public async Task<ItemDto?> GetByIdAsync(Guid id)
        {
            var item = await _unitOfWork.Repository<Item>().GetByIdAsync(id);
            return item == null ? null : _mapper.Map<ItemDto>(item);
        }

        public async Task<ItemDto> CreateAsync(CreateItemDto dto)
        {
            var item = _mapper.Map<Item>(dto);

            await _unitOfWork.Repository<Item>().AddAsync(item);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ItemDto>(item);
        }
        public async Task<ItemDto?> UpdateAsync(UpdateItemDto dto)
        {
            var item = await _unitOfWork.Repository<Item>().GetByIdAsync(dto.Id);
            if (item == null) return null;

            _mapper.Map(dto, item);

            _unitOfWork.Repository<Item>().Update(item);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ItemDto>(item);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _unitOfWork.Repository<Item>().GetByIdAsync(id);
            if (item == null) return false;

            _unitOfWork.Repository<Item>().Delete(item);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}
