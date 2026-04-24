using DiceBound.DTOs.Item;

namespace DiceBound.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllAsync();
        Task<ItemDto?> GetByIdAsync(Guid id);
        Task<ItemDto> CreateAsync(CreateItemDto dto);
        Task<ItemDto?> UpdateAsync(UpdateItemDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
