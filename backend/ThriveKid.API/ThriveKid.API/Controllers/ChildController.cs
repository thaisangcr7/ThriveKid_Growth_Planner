using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThriveKid.API.DTOs.Children;
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
        public async Task<ActionResult<ChildDto>> CreateChild([FromBody] CreateChildDto createDto)
        {
            if (createDto == null)
                return BadRequest("Child data is required.");

            var child = await _childService.CreateChildAsync(createDto);

            if (child == null)
                return StatusCode(500, "An error occurred while creating the child.");

            return CreatedAtAction(nameof(GetChildById), new { id = child.Id }, child);
        }


        // ✅ PUT: api/child/{id}
        // Updates an existing child. Returns 400 if ID mismatch, 404 if not found
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChild(int id, UpdateChildDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest("ID mismatch."); // 400 Bad Request if ID in URL ≠ ID in body

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _childService.UpdateAsync(id, updateDto);
            if (!result)
                return NotFound(); // 404 if no child found

            return NoContent(); // 204 No Content if successful
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
