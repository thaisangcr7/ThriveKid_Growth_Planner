using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.FeedingLogs;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingLogsController : ControllerBase
    {
        private readonly IFeedingLogService _service;

        public FeedingLogsController(IFeedingLogService service)
        {
            _service = service;
        }

        // GET: api/feedinglogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedingLogDto>>> GetAll()
        {
            var logs = await _service.GetAllAsync();
            return Ok(logs);
        }

        // GET: api/feedinglogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedingLogDto>> GetById(int id)
        {
            var log = await _service.GetByIdAsync(id);
            if (log == null)
                return NotFound();

            return Ok(log);
        }

        // POST: api/feedinglogs?childId=1
        [HttpPost]
        public async Task<ActionResult<FeedingLogDto>> Create([FromBody] CreateFeedingLogDto dto, [FromQuery] int childId)
        {
            var created = await _service.CreateAsync(dto, childId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/feedinglogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFeedingLogDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        // DELETE: api/feedinglogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
