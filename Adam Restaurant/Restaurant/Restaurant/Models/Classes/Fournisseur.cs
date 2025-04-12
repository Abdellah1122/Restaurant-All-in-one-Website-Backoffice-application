namespace Restaurant.Models.Classes
{
	public class Fournisseur
	{
		public int Id { get; set; }
		public string Nom { get; set; } = string.Empty;
		public string Prenom { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public CategorieFournisseur categorie {  get; set; }

	}
}
