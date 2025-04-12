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
    public class RestoesController : ControllerBase
    {
        private readonly DataContext _context;

        public RestoesController(DataContext context)
        {
            _context = context;
        }

		// GET: api/Restoes
		[Authorize]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<Resto>>> GetRestos()
        {
            return await _context.Restos.ToListAsync();
        }

    }
}
