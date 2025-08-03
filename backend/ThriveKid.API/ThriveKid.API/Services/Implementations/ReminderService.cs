using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.DTOs.Reminders;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations;

public class ReminderService : IReminderService
{
    private readonly ThriveKidContext _context;

    public ReminderService(ThriveKidContext context)
    {
        _context = context;
    }

    public async Task<List<ReminderDto>> GetAllRemindersAsync()
    {
        return await _context.Reminders
            .Include(r => r.Child)
            .Select(r => new ReminderDto
            {
                Id = r.Id,
                Title = r.Title,
                ReminderTime = r.ReminderTime,
                Notes = r.Notes,
                ChildId = r.ChildId,
                ChildName = r.Child!.FirstName + " " + r.Child.LastName
            })
            .ToListAsync();
    }

    public async Task<ReminderDto?> GetReminderByIdAsync(int id)
    {
        var r = await _context.Reminders
            .Include(r => r.Child)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (r == null) return null;

        return new ReminderDto
        {
            Id = r.Id,
            Title = r.Title,
            ReminderTime = r.ReminderTime,
            Notes = r.Notes,
            ChildId = r.ChildId,
            ChildName = r.Child!.FirstName + " " + r.Child.LastName
        };
    }

    public async Task<ReminderDto> CreateReminderAsync(CreateReminderDto dto)
    {
        var reminder = new Reminder
        {
            Title = dto.Title,
            ReminderTime = dto.ReminderTime,
            Notes = dto.Notes,
            ChildId = dto.ChildId
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var child = await _context.Children.FindAsync(reminder.ChildId);

        return new ReminderDto
        {
            Id = reminder.Id,
            Title = reminder.Title,
            ReminderTime = reminder.ReminderTime,
            Notes = reminder.Notes,
            ChildId = reminder.ChildId,
            ChildName = child != null ? child.FirstName + " " + child.LastName : ""
        };
    }

    public async Task<ReminderDto?> UpdateReminderAsync(int id, UpdateReminderDto dto)
    {
        var reminder = await _context.Reminders.FindAsync(id);
        if (reminder == null) return null;

        reminder.Title = dto.Title;
        reminder.ReminderTime = dto.ReminderTime;
        reminder.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        var child = await _context.Children.FindAsync(reminder.ChildId);

        return new ReminderDto
        {
            Id = reminder.Id,
            Title = reminder.Title,
            ReminderTime = reminder.ReminderTime,
            Notes = reminder.Notes,
            ChildId = reminder.ChildId,
            ChildName = child != null ? child.FirstName + " " + child.LastName : ""
        };
    }

    public async Task<bool> DeleteReminderAsync(int id)
    {
        var reminder = await _context.Reminders.FindAsync(id);
        if (reminder == null) return false;

        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync();

        return true;
    }
}
