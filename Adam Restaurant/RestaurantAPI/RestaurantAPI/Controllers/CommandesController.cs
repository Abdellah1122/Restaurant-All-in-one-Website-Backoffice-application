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
    public class CommandesController : ControllerBase
    {
        private readonly DataContext _context;

        public CommandesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Commandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetCommandes()
        {
            return await _context.Commandes
            .Include(c => c.platTables) 
                .ThenInclude(pt => pt.table)
            .Include(c => c.platTables) 
                .ThenInclude(pt => pt.plat)
                    .ThenInclude(p => p.categorie)
            .ToListAsync();
        }

        // GET: api/Commandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commande>> GetCommande(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);

            if (commande == null)
            {
                return NotFound();
            }

            return commande;
        }

        // PUT: api/Commandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommande(int id, Commande commande)
        {
            if (id != commande.Id)
            {
                return BadRequest();
            }

            _context.Entry(commande).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandeExists(id))
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

        // POST: api/Commandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            try
            {
                
                var platTableIds = commande.platTables.Select(pt => pt.Id).ToList();

                
                var existingPlatTables = await _context.PlatTables
                    .Include(pt => pt.plat) 
                    .Include(pt => pt.table) 
                    .Where(pt => platTableIds.Contains(pt.Id))
                    .ToListAsync();

                
                commande.platTables = existingPlatTables;
                commande.TotalCommande = commande.platTables.Sum(pt => pt.Total);
                _context.Commandes.Add(commande);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCommande", new { id = commande.Id }, commande);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

		// DELETE: api/Commandes/5
		// DELETE: api/Commandes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCommande(int id)
		{
			// Fetch the Commande along with its related PlatTable entities
			var commande = await _context.Commandes
				.Include(c => c.platTables)  // Include PlatTables related to the Commande
				.FirstOrDefaultAsync(c => c.Id == id);

			if (commande == null)
			{
				return NotFound();
			}

			// Remove related PlatTable entries manually
			_context.PlatTables.RemoveRange(commande.platTables);

			// Remove the Commande
			_context.Commandes.Remove(commande);

			// Save changes to the database
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CommandeExists(int id)
        {
            return _context.Commandes.Any(e => e.Id == id);
        }
    }
}
