using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_dotnet.Data;
using backend_dotnet.Models;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PayrollController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Payrolls.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var item = await _db.Payrolls.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Payroll model)
        {
            _db.Payrolls.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Payroll model)
        {
            var existing = await _db.Payrolls.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Employe = model.Employe;
            existing.Mois = model.Mois;
            existing.Montant = model.Montant;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _db.Payrolls.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Payrolls.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
