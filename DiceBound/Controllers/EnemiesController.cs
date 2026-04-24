using DiceBound.DTOs.Enemy;
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiceBound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EnemiesController : ControllerBase
    {
        private readonly IEnemyService _enemyService;

        public EnemiesController(IEnemyService enemyService)
        {
            _enemyService = enemyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnemyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
            => Ok(await _enemyService.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnemyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var enemy = await _enemyService.GetByIdAsync(id);

            if (enemy == null)
                return NotFound();

            return Ok(enemy);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EnemyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateEnemyDto dto)
        {
            var result = await _enemyService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(EnemyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateEnemyDto dto)
        {
            var result = await _enemyService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _enemyService.DeleteAsync(id);
            return Ok(result);
        }
    }
}