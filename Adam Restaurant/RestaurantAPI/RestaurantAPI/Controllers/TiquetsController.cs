using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models.Classes;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiquetsController : ControllerBase
    {
        private readonly DataContext _context;

        public TiquetsController(DataContext context)
        {
            _context = context;
        }

		// GET: api/Tiquets
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Tiquet>>> GetTiquets()
        {
            return await _context.Tiquets
                .Include(c=>c.table)
                .ToListAsync();
        }

		// GET: api/Tiquets/5
		[Authorize]
		[HttpGet("{id}")]
        public async Task<ActionResult<Tiquet>> GetTiquet(int id)
        {
            var tiquet = await _context.Tiquets.FindAsync(id);

            if (tiquet == null)
            {
                return NotFound();
            }

            return tiquet;
        }

        // POST: api/Tiquets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tiquet>> PostTiquet(Tiquet tiquet)
        {
			var excistingTable = _context.Tables.FirstOrDefault(t => t.Id == tiquet.table.Id);
			tiquet.table = excistingTable;
			_context.Tiquets.Add(tiquet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTiquet", new { id = tiquet.Id }, tiquet);
        }
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTiquet(int id)
		{
			Tiquet tiquet = await _context.Tiquets.FindAsync(id);
			_context.Tiquets.Remove(tiquet);
			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
