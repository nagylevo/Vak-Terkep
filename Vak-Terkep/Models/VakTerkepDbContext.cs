using Microsoft.EntityFrameworkCore;

namespace Vak_Terkep.Models
{
    public class VakTerkepDbContext : DbContext
    {
		
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Save> Saved { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VakTerkep");
            } 
        }

    }
}
