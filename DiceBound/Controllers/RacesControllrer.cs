using DiceBound.DTOs.Race;
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiceBound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RacesController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public RacesController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RaceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
            => Ok(await _raceService.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RaceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var race = await _raceService.GetByIdAsync(id);

            if (race == null)
                return NotFound();

            return Ok(race);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RaceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRaceDto dto)
        {
            var result = await _raceService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(RaceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateRaceDto dto)
        {
            var result = await _raceService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _raceService.DeleteAsync(id);
            return Ok(result);
        }
    }
}