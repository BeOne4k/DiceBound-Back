using DiceBound.DTOs.Character;
using DiceBound.DTOs.Item;
using DiceBound.Entities.Enums;
using DiceBound.Entity_s.Characters;
using DiceBound.Entity_s.Items;
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiceBound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IUnitOfWork _unitOfWork;

        public CharactersController(ICharacterService characterService, IUnitOfWork unitOfWork)
        {
            _characterService = characterService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CharacterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            Console.WriteLine(">>> GetAll called");
            return Ok(await _characterService.GetAllAsync());
        }

        // ВАЖНО: "my" должен быть выше "{id}" чтобы не конфликтовать
        [HttpGet("my")]
        [Authorize]
        [ProducesResponseType(typeof(List<CharacterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyCharacters()
        {
            Console.WriteLine(">>> GetMyCharacters called");
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine(">>> userIdStr: " + userIdStr);
            if (userIdStr == null || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();
            var result = await _characterService.GetByUserIdAsync(userId);
            Console.WriteLine(">>> result count: " + result.Count);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var character = await _characterService.GetByIdAsync(id);
            if (character == null)
                return NotFound();
            return Ok(character);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateCharacterDto dto)
        {
            // Берём userId из токена, не доверяем фронту
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            dto.UserId = userId;

            var result = await _characterService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateCharacterDto dto)
        {
            var result = await _characterService.UpdateAsync(dto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}/inventory")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<InventoryItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInventory(Guid id)
        {
            var inventoryItems = await _unitOfWork.Repository<InventoryItem>()
                .Query()
                .Where(inv => inv.CharacterId == id)
                .Include(inv => inv.Item)
                .Select(inv => new InventoryItemDto
                {
                    Id         = inv.Id,
                    ItemId     = inv.ItemId,
                    Name       = inv.Item.Name,
                    Type       = inv.Item.Type.ToString(),
                    Rarity     = inv.Item.Rarity.ToString(),
                    DiceCount  = inv.Item.DiceCount,
                    DiceSides  = inv.Item.DiceSides,
                    Modifier   = inv.Item.Modifier,
                    IsEquipped = inv.IsEquipped,
                    Quantity   = inv.Quantity
                })
                .ToListAsync();

            return Ok(inventoryItems);
        }

        // POST api/Characters/{characterId}/inventory/{inventoryItemId}/equip
        [HttpPost("{characterId}/inventory/{inventoryItemId}/equip")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EquipItem(Guid characterId, Guid inventoryItemId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var character = await _unitOfWork.Repository<Character>().GetByIdAsync(characterId);
            if (character == null || character.UserId != userId)
                return Forbid();

            var invItem = await _unitOfWork.Repository<InventoryItem>()
                .Query()
                .Include(i => i.Item)
                .FirstOrDefaultAsync(i => i.Id == inventoryItemId && i.CharacterId == characterId);

            if (invItem == null) return NotFound();
            if (invItem.IsEquipped) return BadRequest("Item is already equipped");

            // Проверяем: нельзя надеть 2 предмета одного типа
            var sameTypeEquipped = await _unitOfWork.Repository<InventoryItem>()
                .Query()
                .Include(i => i.Item)
                .AnyAsync(i => i.CharacterId == characterId
                            && i.IsEquipped
                            && i.Item.Type == invItem.Item.Type);

            if (sameTypeEquipped)
                return BadRequest($"A {invItem.Item.Type} is already equipped. Unequip it first.");

            // Применяем бафф к персонажу
            ApplyItemBuff(character, invItem.Item, equip: true);

            invItem.IsEquipped = true;
            _unitOfWork.Repository<InventoryItem>().Update(invItem);
            _unitOfWork.Repository<Character>().Update(character);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = $"{invItem.Item.Name} equipped", modifier = invItem.Item.Modifier, type = invItem.Item.Type.ToString() });
        }

        // POST api/Characters/{characterId}/inventory/{inventoryItemId}/unequip
        [HttpPost("{characterId}/inventory/{inventoryItemId}/unequip")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnequipItem(Guid characterId, Guid inventoryItemId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var character = await _unitOfWork.Repository<Character>().GetByIdAsync(characterId);
            if (character == null || character.UserId != userId)
                return Forbid();

            var invItem = await _unitOfWork.Repository<InventoryItem>()
                .Query()
                .Include(i => i.Item)
                .FirstOrDefaultAsync(i => i.Id == inventoryItemId && i.CharacterId == characterId);

            if (invItem == null) return NotFound();
            if (!invItem.IsEquipped) return BadRequest("Item is not equipped");

            // Снимаем бафф
            ApplyItemBuff(character, invItem.Item, equip: false);

            invItem.IsEquipped = false;
            _unitOfWork.Repository<InventoryItem>().Update(invItem);
            _unitOfWork.Repository<Character>().Update(character);
            await _unitOfWork.SaveAsync();

            return Ok(new { message = $"{invItem.Item.Name} unequipped" });
        }

        private static void ApplyItemBuff(Character character, Item item, bool equip)
        {
            int sign = equip ? 1 : -1;
            switch (item.Type)
            {
                case DiceBound.Entities.Enums.ItemType.Weapon:
                    character.AttackBonus += sign * item.Modifier;
                    break;
                case DiceBound.Entities.Enums.ItemType.Armor:
                    character.ArmorClass += sign * item.Modifier;
                    break;
                case DiceBound.Entities.Enums.ItemType.Accessory:
                    character.HP += sign * item.Modifier;
                    break;
            }
        }

        [HttpDelete("{characterId}/inventory/{inventoryItemId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveInventoryItem(Guid characterId, Guid inventoryItemId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            // Проверяем что персонаж принадлежит этому юзеру
            var character = await _unitOfWork.Repository<Character>().GetByIdAsync(characterId);
            if (character == null || character.UserId != userId)
                return Forbid();

            var invItem = await _unitOfWork.Repository<InventoryItem>().GetByIdAsync(inventoryItemId);
            if (invItem == null || invItem.CharacterId != characterId)
                return NotFound();

            _unitOfWork.Repository<InventoryItem>().Delete(invItem);
            await _unitOfWork.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _characterService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return Ok();
        }
    }
}