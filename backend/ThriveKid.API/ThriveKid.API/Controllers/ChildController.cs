using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.Children;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ChildController : ControllerBase
    {
        private readonly IChildService _service;
        public ChildController(IChildService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChildDto>), 200)]
        public async Task<ActionResult<IEnumerable<ChildDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ChildDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ChildDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ChildDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ChildDto>> Create([FromBody] CreateChildDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateChildDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}