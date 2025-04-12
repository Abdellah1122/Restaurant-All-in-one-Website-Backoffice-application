using RestaurantAPI.Models.Enums;

namespace RestaurantAPI.Models.Classes
{
	public class Commande
	{
		public int Id { get; set; }
		public List<PlatTable> platTables { get; set; }
		public double TotalCommande { get; set; }
		

	}

}
