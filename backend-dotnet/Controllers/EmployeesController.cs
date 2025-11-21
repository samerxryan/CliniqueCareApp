using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_dotnet.Data;
using backend_dotnet.Models;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EmployeesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _db.Employees.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var item = await _db.Employees.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee model)
        {
            _db.Employees.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Employee model)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) return NotFound();

            // Simple mapping - in a real app use AutoMapper or similar
            existing.Email = model.Email;
            existing.Adresse = model.Adresse;
            existing.Telephone = model.Telephone;
            existing.Nom = model.Nom;
            existing.Prenom = model.Prenom;
            existing.Cin = model.Cin;
            existing.DateNaissance = model.DateNaissance;
            existing.Poste = model.Poste;
            existing.Salaire = model.Salaire;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Employees.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
