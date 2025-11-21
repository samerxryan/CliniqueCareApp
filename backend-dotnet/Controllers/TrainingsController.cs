using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_dotnet.Data;
using backend_dotnet.Models;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TrainingsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Trainings.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var item = await _db.Trainings.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Training model)
        {
            _db.Trainings.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Training model)
        {
            var existing = await _db.Trainings.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Titre = model.Titre;
            existing.Employe = model.Employe;
            existing.Date = model.Date;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _db.Trainings.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Trainings.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
