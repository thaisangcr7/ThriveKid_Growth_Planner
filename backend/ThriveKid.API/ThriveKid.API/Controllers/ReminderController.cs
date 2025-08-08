using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.Reminders;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reminders = await _reminderService.GetAllAsync();
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reminder = await _reminderService.GetByIdAsync(id);
            if (reminder == null) return NotFound();
            return Ok(reminder);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReminderDto dto)
        {
            var created = await _reminderService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateReminderDto dto)
        {
            var updated = await _reminderService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reminderService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/completed")]
        public async Task<IActionResult> SetCompleted(int id, [FromQuery] bool isCompleted)
        {
            var result = await _reminderService.SetCompletedAsync(id, isCompleted);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}