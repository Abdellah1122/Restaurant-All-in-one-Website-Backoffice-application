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
    public class CategorieFournisseursController : ControllerBase
    {
        private readonly DataContext _context;

        public CategorieFournisseursController(DataContext context)
        {
            _context = context;
        }

		// GET: api/CategorieFournisseurs
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<CategorieFournisseur>>> GetCategorieFournisseurs()
        {
            return await _context.CategorieFournisseurs.ToListAsync();
        }

		// GET: api/CategorieFournisseurs/5
		[Authorize]
		[HttpGet("{id}")]
        public async Task<ActionResult<CategorieFournisseur>> GetCategorieFournisseur(int id)
        {
            var categorieFournisseur = await _context.CategorieFournisseurs.FindAsync(id);

            if (categorieFournisseur == null)
            {
                return NotFound();
            }

            return categorieFournisseur;
        }

		// PUT: api/CategorieFournisseurs/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPut("{id}")]
        public async Task<IActionResult> PutCategorieFournisseur(int id, CategorieFournisseur categorieFournisseur)
        {
            if (id != categorieFournisseur.Id)
            {
                return BadRequest();
            }

            _context.Entry(categorieFournisseur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieFournisseurExists(id))
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

		// POST: api/CategorieFournisseurs
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPost]
        public async Task<ActionResult<CategorieFournisseur>> PostCategorieFournisseur(CategorieFournisseur categorieFournisseur)
        {
            _context.CategorieFournisseurs.Add(categorieFournisseur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorieFournisseur", new { id = categorieFournisseur.Id }, categorieFournisseur);
        }

		// DELETE: api/CategorieFournisseurs/5
		[Authorize]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorieFournisseur(int id)
        {
            var categorieFournisseur = await _context.CategorieFournisseurs.FindAsync(id);
            if (categorieFournisseur == null)
            {
                return NotFound();
            }

            _context.CategorieFournisseurs.Remove(categorieFournisseur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategorieFournisseurExists(int id)
        {
            return _context.CategorieFournisseurs.Any(e => e.Id == id);
        }
    }
}
