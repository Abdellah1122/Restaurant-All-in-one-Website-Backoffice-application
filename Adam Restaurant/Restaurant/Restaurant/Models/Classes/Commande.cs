using Restaurant.Models.Enums;

namespace Restaurant.Models.Classes
{
    public class Commande
    {
        public int Id { get; set; }
        public List<PlatTable> platTables { get; set; }
        public double TotalCommande { get; set; }

	}
}
