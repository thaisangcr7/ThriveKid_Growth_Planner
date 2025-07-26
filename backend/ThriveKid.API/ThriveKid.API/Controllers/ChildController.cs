using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;

        // 👇 Constructor: injects the IChildService interface (resolved by DI container to use ChildService)
        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        // ✅ GET: api/child
        // Fetches all children from the database via the service layer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Child>>> GetAllChildren()
        {
            var children = await _childService.GetAllAsync();
            return Ok(children); // returns 200 OK with list of children
        }

        // ✅ GET: api/child/{id}
        // Fetch a specific child by ID; returns 404 if not found
        [HttpGet("{id}")]
        public async Task<ActionResult<Child>> GetChildById(int id)
        {
            var child = await _childService.GetByIdAsync(id);
            if (child == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(child); // 200 OK with child data
        }

        // ✅ POST: api/child
        // Creates a new child record
        [HttpPost]
        public async Task<ActionResult<Child>> CreateChild(Child child)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // sends validation errors back to client

            var createdChild = await _childService.CreateAsync(child);

            // Returns 201 Created with a location header pointing to the new resource
            return CreatedAtAction(nameof(GetChildById), new { id = createdChild.Id }, createdChild);
        }

        // ✅ PUT: api/child/{id}
        // Updates an existing child. Returns 400 if ID mismatch, 404 if not found
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChild(int id, Child updatedChild)
        {
            if (id != updatedChild.Id)
                return BadRequest("ID mismatch."); // 400 Bad Request if path ID ≠ body ID

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request if model validation fails
            
            // Call service to update child
            var result = await _childService.UpdateAsync(id, updatedChild);
            if (!result)
                return NotFound(); // 404 if no child with this ID

            return NoContent(); // 204 No Content (success but no response body)
        }

        // ✅ DELETE: api/child/{id}
        // Deletes a child by ID. Returns 404 if not found
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            var result = await _childService.DeleteAsync(id);
            if (!result)
                return NotFound(); // 404 Not Found

            return NoContent(); // 204 No Content
        }
    }
}
