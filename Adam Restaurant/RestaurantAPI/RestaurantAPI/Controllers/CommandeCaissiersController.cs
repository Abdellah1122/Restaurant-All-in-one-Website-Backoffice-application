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
    public class CommandeCaissiersController : ControllerBase
    {
        private readonly DataContext _context;

        public CommandeCaissiersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CommandeCaissiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandeCaissier>>> GetCommandeCaissiers()
        {
            return await _context.CommandeCaissiers
                .Include(c=>c.table)
                .ToListAsync();
        }

        // GET: api/CommandeCaissiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeCaissier>> GetCommandeCaissier(int id)
        {
            var commandeCaissier = await _context.CommandeCaissiers.FindAsync(id);

            if (commandeCaissier == null)
            {
                return NotFound();
            }

            return commandeCaissier;
        }

        // PUT: api/CommandeCaissiers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandeCaissier(int id, CommandeCaissier commandeCaissier)
        {
            if (id != commandeCaissier.Id)
            {
                return BadRequest();
            }

            _context.Entry(commandeCaissier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandeCaissierExists(id))
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

        // POST: api/CommandeCaissiers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommandeCaissier>> PostCommandeCaissier(CommandeCaissier commandeCaissier)
        {
            var tableExc = _context.Tables.FirstOrDefault(c => c.Id == commandeCaissier.table.Id);
            commandeCaissier.table = tableExc;
            _context.CommandeCaissiers.Add(commandeCaissier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommandeCaissier", new { id = commandeCaissier.Id }, commandeCaissier);
        }

        // DELETE: api/CommandeCaissiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommandeCaissier(int id)
        {
            var commandeCaissier = await _context.CommandeCaissiers.FindAsync(id);
            if (commandeCaissier == null)
            {
                return NotFound();
            }

            _context.CommandeCaissiers.Remove(commandeCaissier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommandeCaissierExists(int id)
        {
            return _context.CommandeCaissiers.Any(e => e.Id == id);
        }
    }
}
