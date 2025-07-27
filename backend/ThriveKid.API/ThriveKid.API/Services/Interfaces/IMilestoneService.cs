using ThriveKid.API.Models;

namespace ThriveKid.API.Services.Interfaces
{
    public interface IMilestoneService
    {
        Task<IEnumerable<Milestone>> GetAllAsync();
        Task<Milestone?> GetByIdAsync(int id);
        Task<Milestone> CreateAsync(Milestone milestone);
        Task<bool> UpdateAsync(int id, Milestone updatedMilestone);
        Task<bool> DeleteAsync(int id);
    }
}

/*
What this code does (in plain English):
We define a contract (called an interface) that says:

“Any class that implements this must have logic for getting, creating, updating, and deleting Milestones.”

Each method:

GetAllAsync() → Returns all milestones

GetByIdAsync(id) → Returns a single milestone

CreateAsync(milestone) → Adds a new milestone

UpdateAsync(id, milestone) → Modifies existing milestone

DeleteAsync(id) → Deletes it

✅ This interface is what our service (MilestoneService.cs) will follow next.


*/