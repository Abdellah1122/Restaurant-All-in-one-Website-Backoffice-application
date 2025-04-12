using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models.Classes;

namespace RestaurantAPI.Data
{
	public class DataContext : IdentityDbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<CategoriePlat> CategoriePlats { get; set; }
		public DbSet<Employee> Employes { get; set; }
		public DbSet<Owner> Owners { get; set; }
		public DbSet<PlatTable> PlatTables { get; set; }
		public DbSet<Resto> Restos { get; set; }
		public DbSet<Plat> Plats { get; set; }
		public DbSet<Table> Tables { get; set; }
		public DbSet<Tiquet> Tiquets { get; set; }
		public DbSet<TiquetArchive> TiquetsArchives{ get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Commande> Commandes { get; set; }
		public DbSet<CommandeCaissier> CommandeCaissiers { get; set; }
		public DbSet<Fournisseur> Fournisseurs { get; set; }
		public DbSet<CategorieFournisseur> CategorieFournisseurs { get; set; }


	}
}
