using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models.Classes;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatTablesController : ControllerBase
    {
        private readonly DataContext _context;

        public PlatTablesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PlatTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatTable>>> GetPlatTables()
        {
            return await _context.PlatTables
				.Include(c => c.table)
				.Include(c => c.plat)
                    .ThenInclude(c=>c.categorie)
                .ToListAsync();
        }

        // GET: api/PlatTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatTable>> GetPlatTable(int id)
        {
            var platTable = await _context.PlatTables.FindAsync(id);

            if (platTable == null)
            {
                return NotFound();
            }

            return platTable;
        }

        [HttpPut("incrementQuantity/{id}")]
        public async Task<IActionResult> IncrementQuantity(int id)
        {
            var platTable = await _context.PlatTables
                .Include(pt => pt.plat)  // Ensure Plat is included
                .FirstOrDefaultAsync(pt => pt.Id == id);

            if (platTable == null || platTable.plat == null)
            {
                return NotFound("Plat or PlatTable not found.");
            }

            // Increment quantity
            platTable.Quantite++;

            // Correctly update the total based on the price and new quantity
            platTable.Total = platTable.plat.PrixPlat * platTable.Quantite;

            // Save changes
            _context.Entry(platTable).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("ADDQuantity/{id}")]
        public async Task<IActionResult> ADDQuantity(int id)
        {
            var platTable = await _context.PlatTables
                .Include(pt => pt.plat)  
                .FirstOrDefaultAsync(pt => pt.Id == id);

            if (platTable == null || platTable.plat == null)
            {
                return NotFound("Plat or PlatTable not found.");
            }
            platTable.Quantite++;
            _context.Entry(platTable).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("MINQuantity/{id}")]
        public async Task<IActionResult> MINQuantity(int id)
        {
            var platTable = await _context.PlatTables
                .Include(pt => pt.plat)
                .FirstOrDefaultAsync(pt => pt.Id == id);

            if (platTable == null || platTable.plat == null)
            {
                return NotFound("Plat or PlatTable not found.");
            }
            platTable.Quantite--;
            _context.Entry(platTable).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/PlatTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatTable(int id, PlatTable platTable)
        {
            if (id != platTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(platTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlatTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlatTable>> PostPlatTable(PlatTable platTable)
        {
			var existingTable = await _context.Tables
				.FirstOrDefaultAsync(c => c.Id == platTable.table.Id);

			var existingPlat = await _context.Plats
				.FirstOrDefaultAsync(c => c.Id == platTable.plat.Id);

			platTable.table = existingTable;
			platTable.plat = existingPlat;

			_context.PlatTables.Add(platTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlatTable", new { id = platTable.Id }, platTable);
        }

        // DELETE: api/PlatTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatTable(int id)
        {
            var platTable = await _context.PlatTables.FindAsync(id);
            if (platTable == null)
            {
                return NotFound();
            }

            _context.PlatTables.Remove(platTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatTableExists(int id)
        {
            return _context.PlatTables.Any(e => e.Id == id);
        }
    }
}
