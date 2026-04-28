using DiceBound.DTOs.Character;
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiceBound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CharacterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
            => Ok(await _characterService.GetAllAsync());

        // ВАЖНО: "my" должен быть выше "{id}" чтобы не конфликтовать
        [HttpGet("my")]
        [Authorize]
        [ProducesResponseType(typeof(List<CharacterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyCharacters()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var result = await _characterService.GetByUserIdAsync(userId);
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