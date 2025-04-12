using Restaurant.Models.Enums;

namespace Restaurant.Models.Classes
{
    public class Booking
    {
        public int Id { get; set; }
        public Client client { get; set; }
        public Table table { get; set; }
        public DateTime DateReservation { get; set; }
		public StatutReservation Statut { get; set; }
	}
}
