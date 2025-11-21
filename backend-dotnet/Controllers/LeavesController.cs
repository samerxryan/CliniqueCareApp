using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_dotnet.Data;
using backend_dotnet.Models;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeavesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public LeavesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Leaves.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var item = await _db.Leaves.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Leave model)
        {
            _db.Leaves.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Leave model)
        {
            var existing = await _db.Leaves.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Employe = model.Employe;
            existing.Type = model.Type;
            existing.Debut = model.Debut;
            existing.Fin = model.Fin;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _db.Leaves.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Leaves.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
