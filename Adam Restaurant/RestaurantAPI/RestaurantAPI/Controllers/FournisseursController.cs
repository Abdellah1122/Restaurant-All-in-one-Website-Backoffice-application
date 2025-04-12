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
    public class FournisseursController : ControllerBase
    {
        private readonly DataContext _context;

        public FournisseursController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Fournisseurs
        [HttpGet]
		[Authorize]
		public async Task<ActionResult<IEnumerable<Fournisseur>>> GetFournisseurs()
        {
            return await _context.Fournisseurs
                .Include(c=>c.categorie)
                .ToListAsync();
        }

		// GET: api/Fournisseurs/5
		[Authorize]
		[HttpGet("{id}")]
        public async Task<ActionResult<Fournisseur>> GetFournisseur(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);

            if (fournisseur == null)
            {
                return NotFound();
            }

            return fournisseur;
        }

		// PUT: api/Fournisseurs/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutFournisseur(int id, Fournisseur fournisseur)
        {
            if (id != fournisseur.Id)
            {
                return BadRequest();
            }

            _context.Entry(fournisseur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FournisseurExists(id))
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

		// POST: api/Fournisseurs
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<Fournisseur>> PostFournisseur(Fournisseur fournisseur)
        {
            var categorieF = _context.CategorieFournisseurs.FirstOrDefault(c => c.Id == fournisseur.categorie.Id);
            fournisseur.categorie = categorieF;
            _context.Fournisseurs.Add(fournisseur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFournisseur", new { id = fournisseur.Id }, fournisseur);
        }

		// DELETE: api/Fournisseurs/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFournisseur(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            _context.Fournisseurs.Remove(fournisseur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FournisseurExists(int id)
        {
            return _context.Fournisseurs.Any(e => e.Id == id);
        }
    }
}
