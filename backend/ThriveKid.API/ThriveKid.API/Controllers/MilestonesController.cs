using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.Models;
using ThriveKid.API.Services;
/* 
| Line                                  | Purpose                                                                               |
| ------------------------------------- | ------------------------------------------------------------------------------------- |
| `IMilestoneService _milestoneService` | This uses the service abstraction you built (good for testing and clean architecture) |
| `GetAll()`                            | GETs a list of all milestones                                                         |
| `GetById(id)`                         | Finds a specific milestone                                                            |
| `Create()`                            | Adds a new milestone                                                                  |
| `Update()`                            | Edits an existing milestone                                                           |
| `Delete()`                            | Removes a milestone by ID                                                             |
 
*/
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

        // GET: api/milestones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Milestone>>> GetAll()
        {
            var milestones = await _milestoneService.GetAllAsync();
            return Ok(milestones);
        }

        // GET: api/milestones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Milestone>> GetById(int id)
        {
            var milestone = await _milestoneService.GetByIdAsync(id);
            if (milestone == null)
                return NotFound();

            return Ok(milestone);
        }

        // POST: api/milestones
        [HttpPost]
        public async Task<ActionResult<Milestone>> Create(Milestone milestone)
        {
            var created = await _milestoneService.CreateAsync(milestone);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/milestones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Milestone milestone)
        {
            if (id != milestone.Id)
                return BadRequest("ID mismatch");

            var success = await _milestoneService.UpdateAsync(id, milestone);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/milestones/5
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
