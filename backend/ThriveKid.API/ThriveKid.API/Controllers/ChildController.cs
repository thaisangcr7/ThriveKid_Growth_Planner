using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.Models;
using ThriveKid.API.Services;

namespace ThriveKid.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;

        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Child>>> GetAllChildren()
        {
            var children = await _childService.GetAllAsync();
            return Ok(children);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Child>> GetChildById(int id)
        {
            var child = await _childService.GetByIdAsync(id);
            if (child == null)
            {
                return NotFound();
            }

            return Ok(child);
        }

        [HttpPost]
        public async Task<ActionResult<Child>> CreateChild(Child child)
        {
            var createdChild = await _childService.CreateAsync(child);
            return CreatedAtAction(nameof(GetChildById), new { id = createdChild.Id }, createdChild);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChild(int id, Child updatedChild)
        {
            var result = await _childService.UpdateAsync(id, updatedChild);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            var result = await _childService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
