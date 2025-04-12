using Restaurant.Models.Enums;

namespace Restaurant.Models.Classes
{
	public class TiquetArchive
	{
		public int Id { get; set; }
		public Table table { get; set; }
		public DateTime DateTiquet { get; set; }
		public ModePayment ModePayment { get; set; }
		public double TotalTiquet { get; set; }
		public double MontantDonne { get; set; }
		public double Reste { get; set; }

	}
}
