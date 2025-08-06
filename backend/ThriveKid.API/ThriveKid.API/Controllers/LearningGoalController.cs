using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.LearningGoals;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearningGoalController : ControllerBase
    {
        private readonly ILearningGoalService _learningGoalService;

        public LearningGoalController(ILearningGoalService learningGoalService)
        {
            _learningGoalService = learningGoalService;
        }

        // GET: api/LearningGoal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LearningGoalDto>>> GetAll()
        {
            var goals = await _learningGoalService.GetAllAsync();
            return Ok(goals);
        }

        // GET: api/LearningGoal/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LearningGoalDto>> GetById(int id)
        {
            var goal = await _learningGoalService.GetByIdAsync(id);
            if (goal == null) return NotFound();
            return Ok(goal);
        }

        // POST: api/LearningGoal
        [HttpPost]
        public async Task<ActionResult<LearningGoalDto>> Create([FromBody] CreateLearningGoalDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _learningGoalService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/LearningGoal/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLearningGoalDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _learningGoalService.UpdateAsync(id, updateDto);
            if (updated == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/LearningGoal/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _learningGoalService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
