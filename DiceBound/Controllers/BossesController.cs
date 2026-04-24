
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiceBound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BossesController : ControllerBase
    {
        private readonly IBossService _bossService;

        public BossesController(IBossService bossService)
        {
            _bossService = bossService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BossDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
            => Ok(await _bossService.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var boss = await _bossService.GetByIdAsync(id);

            if (boss == null)
                return NotFound();

            return Ok(boss);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateBossDto dto)
        {
            var result = await _bossService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateBossDto dto)
        {
            var result = await _bossService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bossService.DeleteAsync(id);
            return Ok();
        }
    }
}