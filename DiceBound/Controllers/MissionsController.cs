using DiceBound.DTOs.Mission;
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiceBound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MissionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
            => Ok(await _missionService.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MissionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var mission = await _missionService.GetByIdAsync(id);

            if (mission == null)
                return NotFound();

            return Ok(mission);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MissionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMissionDto dto)
        {
            var result = await _missionService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(MissionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateMissionDto dto)
        {
            var result = await _missionService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _missionService.DeleteAsync(id);
            return Ok(result);
        }
    }
}