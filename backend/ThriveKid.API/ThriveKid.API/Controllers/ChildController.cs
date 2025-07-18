using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;

namespace ThriveKid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly ThriveKidContext _context;

        public ChildController(ThriveKidContext context)
        {
            _context = context;
        }

        // GET: api/child
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Child>>> GetAll()
        {
            return await _context.Children.ToListAsync();
        }

        // GET: api/child/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Child>> GetById(int id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null)
                return NotFound();

            return child;
        }

        // POST: api/child
        [HttpPost]
        public async Task<ActionResult<Child>> Create(Child child)
        {
            _context.Children.Add(child);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = child.Id }, child);
        }

        // PUT: api/child/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChild(int id, [FromBody] Child updatedChild)
        {
            if (id != updatedChild.Id)
                return BadRequest("ID mismatch.");

            var existingChild = await _context.Children.FindAsync(id);
            if (existingChild == null)
                return NotFound();

            existingChild.FirstName = updatedChild.FirstName;
            existingChild.LastName = updatedChild.LastName;
            existingChild.DateOfBirth = updatedChild.DateOfBirth;
            existingChild.Gender = updatedChild.Gender;
            existingChild.AgeInMonths = updatedChild.AgeInMonths;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/child/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null)
                return NotFound();

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}