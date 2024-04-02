using FoodOffer.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FoodOffer.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        //Esta instancia esta solo para correr las migraciones a la base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<LoginData> LoginDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasKey(a => new { a.Client_Id, a.Item });

            base.OnModelCreating(modelBuilder);
        }
    }
}