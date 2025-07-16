using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThriveKid.API.Models;

namespace ThriveKid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private static List<Child> _children = new();

        [HttpGet]
        public ActionResult<IEnumerable<Child>> GetAll()
        {
            return Ok(_children);
        }

        [HttpGet("{id}")]
        public ActionResult<Child> GetById(int id)
        {
            var child = _children.FirstOrDefault(c => c.Id == id);
            if (child == null) return NotFound();
            return Ok(child);
        }

        [HttpPost]
        public ActionResult<Child> Create(Child child)
        {
            child.Id = _children.Count + 1;
            _children.Add(child);
            return CreatedAtAction(nameof(GetById), new { id = child.Id }, child);
        }
    }
}
