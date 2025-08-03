using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.Reminders;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReminderController : ControllerBase
{
    private readonly IReminderService _reminderService;

    public ReminderController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    // GET: api/Reminder
    [HttpGet]
    public async Task<ActionResult<List<ReminderDto>>> GetAll()
    {
        var reminders = await _reminderService.GetAllRemindersAsync();
        return Ok(reminders);
    }

    // GET: api/Reminder/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ReminderDto>> GetById(int id)
    {
        var reminder = await _reminderService.GetReminderByIdAsync(id);
        if (reminder == null)
            return NotFound();

        return Ok(reminder);
    }

    // POST: api/Reminder
    [HttpPost]
    public async Task<ActionResult<ReminderDto>> Create(CreateReminderDto dto)
    {
        var created = await _reminderService.CreateReminderAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/Reminder/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<ReminderDto>> Update(int id, UpdateReminderDto dto)
    {
        var updated = await _reminderService.UpdateReminderAsync(id, dto);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    // DELETE: api/Reminder/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _reminderService.DeleteReminderAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
