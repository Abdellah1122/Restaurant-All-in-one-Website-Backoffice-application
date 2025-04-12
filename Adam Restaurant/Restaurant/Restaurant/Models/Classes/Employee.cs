namespace Restaurant.Models.Classes
{
    public class Employee
    {
        public int Id { get; set; }
        public string CIN { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public double Salaire { get; set; }
    }
}
