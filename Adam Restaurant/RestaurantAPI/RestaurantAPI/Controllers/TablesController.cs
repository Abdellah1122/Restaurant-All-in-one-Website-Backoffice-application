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
using RestaurantAPI.Models.Enums;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class TablesController : ControllerBase
	{
		private readonly DataContext _context;

		public TablesController(DataContext context)
		{
			_context = context;
		}

        // GET: api/Tables
        [HttpGet]
		public async Task<ActionResult<IEnumerable<Table>>> GetTables()
		{
			return await _context.Tables
				.ToListAsync();
		}

		// GET: api/Tables/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Table>> GetTable(int id)
		{
			var table = await _context.Tables.FindAsync(id);

			if (table == null)
			{
				return NotFound();
			}

			return table;
		}

		// PUT: api/Tables/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTable(int id, Table table)
		{
			if (id != table.Id)
			{
				return BadRequest();
			}

			_context.Entry(table).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TableExists(id))
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
		// PUT: api/Tables/5/Status/Vide
		
		[HttpPut("{id}/Status/Vide")]
		public async Task<IActionResult> SetTableStatusVide(int id)
		{
			var table = await _context.Tables.FindAsync(id);
			if (table == null)
			{
				return NotFound();
			}

			table.statut = StatutTable.Vide;
			_context.Entry(table).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// PUT: api/Tables/5/Status/Réserver
		
		[HttpPut("{id}/Status/Réserver")]
		public async Task<IActionResult> SetTableStatusRéserver(int id)
		{
			var table = await _context.Tables.FindAsync(id);
			if (table == null)
			{
				return NotFound();
			}

			table.statut = StatutTable.Reserve;
			_context.Entry(table).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// PUT: api/Tables/5/Status/Plein_Servis
		
		[HttpPut("{id}/Status/Plein_Servis")]
		public async Task<IActionResult> SetTableStatusPleinServis(int id)
		{
			var table = await _context.Tables.FindAsync(id);
			if (table == null)
			{
				return NotFound();
			}

			table.statut = StatutTable.Plein_Servis;
			_context.Entry(table).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// PUT: api/Tables/5/Status/Plein_NonServis
		
		[HttpPut("{id}/Status/Plein_NonServis")]
		public async Task<IActionResult> SetTableStatusPleinNonServis(int id)
		{
			var table = await _context.Tables.FindAsync(id);
			if (table == null)
			{
				return NotFound();
			}

			table.statut = StatutTable.Plein_NonServis;
			_context.Entry(table).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}
		//
		[HttpPut("IncrementNbrFoisReserve")]
		public async Task<IActionResult> IncrementNbrFoisReserve(int id)
		{
			var table = await _context.Tables.FindAsync(id);
			if (table == null)
			{
				return NotFound();
			}

			table.NbrFoisReserve++;
			_context.Entry(table).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}
		//
		[HttpPut("IncrementNbrFoisOccupe")]
		public async Task<IActionResult> IncrementNbrFoisOccupe(int id)
		{
			var table = await _context.Tables.FindAsync(id);
			if (table == null)
			{
				return NotFound();
			}

			table.NbrFoisOccupe++;
			_context.Entry(table).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}
		// POST: api/Tables
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[Authorize]
		[HttpPost]
		public async Task<ActionResult<Table>> PostTable(Table table)
		{
			_context.Tables.Add(table);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTable", new { id = table.Id }, table);
		}

		// DELETE: api/Tables/5
		[Authorize]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTable(int id)
		{
			var table = await _context.Tables.FindAsync(id);
			if (table == null)
			{
				return NotFound();
			}

			_context.Tables.Remove(table);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TableExists(int id)
		{
			return _context.Tables.Any(e => e.Id == id);
		}
	}
}
