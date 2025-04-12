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
    public class CategoriePlatsController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriePlatsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CategoriePlats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriePlat>>> GetCategoriePlats()
        {
            return await _context.CategoriePlats.ToListAsync();
        }

        // GET: api/CategoriePlats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriePlat>> GetCategoriePlat(int id)
        {
            var categoriePlat = await _context.CategoriePlats.FindAsync(id);

            if (categoriePlat == null)
            {
                return NotFound();
            }

            return categoriePlat;
        }

		// PUT: api/CategoriePlats/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriePlat(int id, CategoriePlat categoriePlat)
        {
            if (id != categoriePlat.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriePlat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriePlatExists(id))
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

		// POST: api/CategoriePlats
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<CategoriePlat>> PostCategoriePlat(CategoriePlat categoriePlat)
        {
            _context.CategoriePlats.Add(categoriePlat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriePlat", new { id = categoriePlat.Id }, categoriePlat);
        }

		// DELETE: api/CategoriePlats/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriePlat(int id)
        {
            var categoriePlat = await _context.CategoriePlats.FindAsync(id);
            if (categoriePlat == null)
            {
                return NotFound();
            }

            _context.CategoriePlats.Remove(categoriePlat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriePlatExists(int id)
        {
            return _context.CategoriePlats.Any(e => e.Id == id);
        }
    }
}
