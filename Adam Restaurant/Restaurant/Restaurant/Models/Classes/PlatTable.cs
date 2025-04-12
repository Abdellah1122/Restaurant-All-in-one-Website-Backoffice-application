namespace Restaurant.Models.Classes
{
    public class PlatTable
    {
        public int Id { get; set; }
        public Plat plat { get; set; }
        public Table table { get; set; }
        public int Quantite { get; set; }
        public double Total { get; set; }
    }
}
