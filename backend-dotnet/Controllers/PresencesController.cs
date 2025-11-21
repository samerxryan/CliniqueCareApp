using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_dotnet.Data;
using backend_dotnet.Models;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresencesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PresencesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Presences.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var item = await _db.Presences.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Presence model)
        {
            _db.Presences.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Presence model)
        {
            var existing = await _db.Presences.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Employe = model.Employe;
            existing.Date = model.Date;
            existing.Status = model.Status;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _db.Presences.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Presences.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
