using Microsoft.AspNetCore.Mvc;                  // For API controller base classes and attributes
using ThriveKid.API.DTOs.FeedingLogs;            // For FeedingLogDto, CreateFeedingLogDto, UpdateFeedingLogDto
using ThriveKid.API.Services.Interfaces;         // For IFeedingLogService interface

namespace ThriveKid.API.Controllers
{
    [Route("api/[controller]")]                  // Route: api/feedinglogs
    [ApiController]                             // Enables automatic model validation and API conventions
    public class FeedingLogsController : ControllerBase
    {
        private readonly IFeedingLogService _service;

        // 👇 Constructor: injects the IFeedingLogService interface (resolved by DI container)
        public FeedingLogsController(IFeedingLogService service)
        {
            _service = service;
        }

        // ✅ GET: api/feedinglogs
        // Fetches all feeding logs from the database via the service layer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedingLogDto>>> GetAll()
        {
            var logs = await _service.GetAllAsync();
            return Ok(logs); // returns 200 OK with list of feeding logs
        }

        // ✅ GET: api/feedinglogs/{id}
        // Fetch a specific feeding log by ID; returns 404 if not found
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedingLogDto>> GetById(int id)
        {
            var log = await _service.GetByIdAsync(id);
            if (log == null)
                return NotFound(); // 404 Not Found

            return Ok(log); // 200 OK with feeding log data
        }

        // ✅ POST: api/feedinglogs?childId=1
        // Creates a new feeding log for a specific child
        [HttpPost]
        public async Task<ActionResult<FeedingLogDto>> Create([FromBody] CreateFeedingLogDto dto, [FromQuery] int childId)
        {
            var created = await _service.CreateAsync(dto, childId);

            // Returns 201 Created with a Location header pointing to the new resource
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ✅ PUT: api/feedinglogs/{id}
        // Updates an existing feeding log. Returns 404 if not found
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFeedingLogDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound(); // 404 if not found

            return NoContent(); // 204 No Content if successful
        }

        // ✅ DELETE: api/feedinglogs/{id}
        // Deletes a feeding log by ID. Returns 404 if not found
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(); // 404 Not Found

            return NoContent(); // 204 No Content if successful
        }
    }
}