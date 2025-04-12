using System.Security.Cryptography.X509Certificates;

namespace Restaurant.Models.Classes
{
	public class ArticleCommande
	{

		public string Nom { get; set; } = string.Empty;
		public string Measurement { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public double PrixUnitaire { get; set; }
		public double PrixTotal { get; set; }
		
	}
}
