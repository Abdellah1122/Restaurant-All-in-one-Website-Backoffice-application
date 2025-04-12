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
    public class TiquetArchivesController : ControllerBase
    {
        private readonly DataContext _context;

        public TiquetArchivesController(DataContext context)
        {
            _context = context;
        }

		// GET: api/TiquetArchives
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<TiquetArchive>>> GetTiquetsArchives()
        {
            return await _context.TiquetsArchives
				.Include(c => c.table)
                .ToListAsync();
        }

		[Authorize]
		[HttpPost]
        public async Task<ActionResult<TiquetArchive>> PostTiquetArchive(TiquetArchive tiquetArchive)
        {
			var excistingTable = _context.Tables.FirstOrDefault(t => t.Id == tiquetArchive.table.Id);
			tiquetArchive.table = excistingTable;
			_context.TiquetsArchives.Add(tiquetArchive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTiquetArchive", new { id = tiquetArchive.Id }, tiquetArchive);
        }

		// DELETE: api/TiquetArchives/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiquetArchive(int id)
        {
            var tiquetArchive = await _context.TiquetsArchives.FindAsync(id);
            if (tiquetArchive == null)
            {
                return NotFound();
            }

            _context.TiquetsArchives.Remove(tiquetArchive);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiquetArchiveExists(int id)
        {
            return _context.TiquetsArchives.Any(e => e.Id == id);
        }
    }
}
