using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.Milestones;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestonesController : ControllerBase
    {
        private readonly IMilestoneService _milestoneService;

        public MilestonesController(IMilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
        }

        // ✅ GET: api/milestones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetAll()
        {
            var milestones = await _milestoneService.GetAllAsync();
            return Ok(milestones);
        }

        // ✅ GET: api/milestones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MilestoneDto>> GetById(int id)
        {
            var milestone = await _milestoneService.GetByIdAsync(id);
            if (milestone == null)
                return NotFound();

            return Ok(milestone);
        }

        // ✅ POST: api/milestones/{childId}
        [HttpPost("{childId}")]
        public async Task<ActionResult<MilestoneDto>> Create(int childId, CreateMilestoneDto dto)
        {
            var created = await _milestoneService.CreateAsync(dto, childId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ✅ PUT: api/milestones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMilestoneDto dto)
        {
            var success = await _milestoneService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // ✅ DELETE: api/milestones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _milestoneService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
