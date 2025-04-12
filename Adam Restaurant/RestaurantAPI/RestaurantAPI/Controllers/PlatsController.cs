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
    public class PlatsController : ControllerBase
    {
        private readonly DataContext _context;

        public PlatsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Plats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plat>>> GetPlats()
        {
            return await _context.Plats
                .Include(c=>c.categorie)
                .ToListAsync();
        }

		// GET: api/Plats/5
		[HttpGet("{id}")]
        public async Task<ActionResult<Plat>> GetPlat(int id)
        {
            var plat = await _context.Plats.FindAsync(id);

            if (plat == null)
            {
                return NotFound();
            }

            return plat;
        }

		// PUT: api/Plats/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutPlat(int id, Plat plat)
        {
            if (id != plat.Id)
            {
                return BadRequest();
            }

            _context.Entry(plat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatExists(id))
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

		// POST: api/Plats
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<Plat>> PostPlat(Plat plat)
        {
            var cat = _context.CategoriePlats.FirstOrDefault(c=>c.Id == plat.categorie.Id);
            plat.categorie = cat;
            _context.Plats.Add(plat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlat", new { id = plat.Id }, plat);
        }

		[Authorize]
		[HttpPut("ChangePrix/{id}")]
		public async Task<IActionResult> ChangePrix(int id,double prix)
		{
			var plat = await _context.Plats.FindAsync(id);  

			if (plat == null)
			{
				return NotFound();
			}

			plat.PrixPlat = prix;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/Plats/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlat(int id)
        {
            var plat = await _context.Plats.FindAsync(id);
            if (plat == null)
            {
                return NotFound();
            }

            _context.Plats.Remove(plat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //
        [HttpPut("IncrementNbrFoisCommande/{id}")]
        public async Task<IActionResult> IncrementNbrFoisCommande(int id)
        {
            var plat = await _context.Plats.FindAsync(id);
            if (plat == null)
            {
                return NotFound();
            }

            plat.NbrFoisCommande++;
            _context.Entry(plat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PlatExists(int id)
        {
            return _context.Plats.Any(e => e.Id == id);
        }
    }
}
