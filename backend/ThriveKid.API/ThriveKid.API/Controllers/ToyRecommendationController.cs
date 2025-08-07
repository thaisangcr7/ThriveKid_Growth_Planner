using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.DTOs.ToyRecommendations;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToyRecommendationController : ControllerBase
    {
        private readonly IToyRecommendationService _service;

        public ToyRecommendationController(IToyRecommendationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToyRecommendationDto>>> GetAll()
        {
            var toys = await _service.GetAllToyRecommendationsAsync();
            return Ok(toys);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToyRecommendationDto>> GetById(int id)
        {
            var toy = await _service.GetToyRecommendationByIdAsync(id);
            if (toy == null)
                return NotFound();
            return Ok(toy);
        }

        [HttpPost]
        public async Task<ActionResult<ToyRecommendationDto>> Create(CreateToyRecommendationDto dto)
        {
            var created = await _service.CreateToyRecommendationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateToyRecommendationDto dto)
        {
            var updated = await _service.UpdateToyRecommendationAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteToyRecommendationAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}