using RestaurantAPI.Models;

namespace RestaurantAPI.Service
{
	public interface IEmailService
	{
		void SendEmail(EmailDTO request);
	}
}
