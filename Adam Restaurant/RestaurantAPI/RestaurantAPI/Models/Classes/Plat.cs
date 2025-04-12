namespace RestaurantAPI.Models.Classes
{
	public class Plat
	{
		public int Id { get; set; }
		public string NomPlat { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public double PrixPlat { get; set; }
		public string ImagePlat { get; set; } = string.Empty;
		public CategoriePlat categorie { get; set; }
		public int NbrFoisCommande { get; set; }
	}
}
