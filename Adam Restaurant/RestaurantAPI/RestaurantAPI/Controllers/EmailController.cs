using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Service;

namespace RestaurantAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		private readonly IEmailService _emailService;

		public EmailController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		// POST: api/Email/send
		[HttpPost("send")]
		public IActionResult SendEmail(EmailDTO request)
		{
			{
				_emailService.SendEmail(request);
				return Ok();
			}
		}
	}
}
