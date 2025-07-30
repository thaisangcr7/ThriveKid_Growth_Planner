using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.SleepLogs;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers

/*
 | Endpoint             | Method | Purpose                 |
| -------------------- | ------ | ----------------------- |
| `/api/SleepLog`      | GET    | Get all sleep logs      |
| `/api/SleepLog/{id}` | GET    | Get one sleep log by ID |
| `/api/SleepLog`      | POST   | Create new sleep log    |
| `/api/SleepLog/{id}` | PUT    | Update sleep log        |
| `/api/SleepLog/{id}` | DELETE | Delete sleep log        |

*/

{
    [ApiController]
    [Route("api/[controller]")]
    public class SleepLogController : ControllerBase
    {
        private readonly ISleepLogService _sleepLogService;

        public SleepLogController(ISleepLogService sleepLogService)
        {
            _sleepLogService = sleepLogService;
        }

        // GET: api/SleepLog
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sleepLogs = await _sleepLogService.GetAllSleepLogsAsync();
            return Ok(sleepLogs);
        }

        // GET: api/SleepLog/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sleepLog = await _sleepLogService.GetSleepLogByIdAsync(id);
            if (sleepLog == null)
                return NotFound();

            return Ok(sleepLog);
        }

        // POST: api/SleepLog
        [HttpPost]
        public async Task<IActionResult> Create(CreateSleepLogDto sleepLogDto)
        {
            var created = await _sleepLogService.CreateSleepLogAsync(sleepLogDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/SleepLog/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSleepLogDto updateDto)
        {
            var success = await _sleepLogService.UpdateSleepLogAsync(id, updateDto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/SleepLog/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _sleepLogService.DeleteSleepLogAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
