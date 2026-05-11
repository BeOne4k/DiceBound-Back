using DiceBound.DTOs.Mission;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Entity_s.Items;
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiceBound.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IUnitOfWork _unitOfWork;

        public MissionsController(IMissionService missionService, IUnitOfWork unitOfWork)
        {
            _missionService = missionService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MissionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _missionService.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MissionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var mission = await _missionService.GetByIdAsync(id);
            if (mission == null) return NotFound();
            return Ok(mission);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MissionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateMissionDto dto)
            => Ok(await _missionService.CreateAsync(dto));

        [HttpPut]
        [ProducesResponseType(typeof(MissionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateMissionDto dto)
            => Ok(await _missionService.UpdateAsync(dto));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
            => Ok(await _missionService.DeleteAsync(id));

        // ── REWARD ITEMS ──────────────────────────────
        // POST api/Missions/{missionId}/rewards  { itemId, quantity }
        [HttpPost("{missionId}/rewards")]
        [ProducesResponseType(typeof(MissionRewardItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddReward(Guid missionId, [FromBody] AddMissionRewardDto dto)
        {
            var mission = await _unitOfWork.Repository<Mission>().GetByIdAsync(missionId);
            if (mission == null) return NotFound("Mission not found");

            var item = await _unitOfWork.Repository<Item>().GetByIdAsync(dto.ItemId);
            if (item == null) return NotFound("Item not found");

            var reward = new MissionRewardItem
            {
                MissionId = missionId,
                ItemId    = dto.ItemId,
                Quantity  = dto.Quantity > 0 ? dto.Quantity : 1
            };

            await _unitOfWork.Repository<MissionRewardItem>().AddAsync(reward);
            await _unitOfWork.SaveAsync();

            return Ok(new MissionRewardItemDto
            {
                Id         = reward.Id,
                ItemId     = item.Id,
                ItemName   = item.Name,
                ItemType   = item.Type.ToString(),
                ItemRarity = item.Rarity.ToString(),
                Quantity   = reward.Quantity
            });
        }

        // DELETE api/Missions/{missionId}/rewards/{rewardId}
        [HttpDelete("{missionId}/rewards/{rewardId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveReward(Guid missionId, Guid rewardId)
        {
            var reward = await _unitOfWork.Repository<MissionRewardItem>().GetByIdAsync(rewardId);
            if (reward == null || reward.MissionId != missionId) return NotFound();

            _unitOfWork.Repository<MissionRewardItem>().Delete(reward);
            await _unitOfWork.SaveAsync();

            return Ok();
        }
    }
}