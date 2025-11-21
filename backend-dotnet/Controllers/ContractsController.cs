using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_dotnet.Data;
using backend_dotnet.Models;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ContractsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Contracts.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var item = await _db.Contracts.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contract model)
        {
            _db.Contracts.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Contract model)
        {
            var existing = await _db.Contracts.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Type = model.Type;
            existing.Employe = model.Employe;
            existing.DateDebut = model.DateDebut;
            existing.DateFin = model.DateFin;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _db.Contracts.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Contracts.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
