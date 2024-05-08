using FoodOffer.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FoodOffer.Infrastructure
{
    public class ApplicationDbContext : DbContext //La clase DbContext es una clase proporcionada por Entity Framework Core y se utiliza para interactuar con la base de datos utilizando este ORM. Proporciona funcionalidades para realizar operaciones de CRUD (Crear, Leer, Actualizar y Eliminar) en la base de datos y mapear objetos de dominio a tablas de base de datos y viceversa.
    {
        //Esta instancia esta solo para correr las migraciones a la base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ClientDb> Client { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<LoginData> LoginDatas { get; set; }

        public DbSet<AdvertisingDb> Advertising { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasKey(a => new { a.Client_Id, a.Item });

            base.OnModelCreating(modelBuilder);
        }
    }
}