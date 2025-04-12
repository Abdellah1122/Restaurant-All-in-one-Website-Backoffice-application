namespace RestaurantAPI.Models.Classes
{
    public class CommandeCaissier
    {
        public int Id { get; set; }
        public Table table { get; set; }
        public double Total { get; set; }
    }
}
