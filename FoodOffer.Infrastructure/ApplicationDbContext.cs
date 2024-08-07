using clasificados.Infraestructure.DbContextConfig.DbModels;
using Microsoft.EntityFrameworkCore;
    // Establece las tablas que van a ser creadas en la base de datos.
namespace FoodOffer.Infrastructure
{
    public class ApplicationDbContext : DbContext //La clase DbContext es una clase proporcionada por Entity Framework Core y se utiliza para interactuar con la base de datos utilizando este ORM. Proporciona funcionalidades para realizar operaciones de CRUD (Crear, Leer, Actualizar y Eliminar) en la base de datos y mapear objetos de dominio a tablas de base de datos y viceversa.
    {
        //Esta instancia esta solo para correr las migraciones a la base de datos
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Db_User> users { get; set; }

        public DbSet<Db_User_Type> user_types { get; set; }

        public DbSet<Db_Address> addresses { get; set; }

        public DbSet<Db_User_Login_Data> user_login_data { get; set; }

        public DbSet<Db_Advertising> advertisings { get; set; }

        public DbSet<Db_Advertising_Attribute> advertising_attributes { get; set; }

        public DbSet<Db_Advertising_Category> advertising_categories { get; set; }

        public DbSet<Db_Advertising_Image> advertising_images { get; set; }

        public DbSet<Db_Advertising_Time_Set> advertising_time_settings { get; set; }

        public DbSet<Db_Advertising_State> advertising_states { get; set; }

        public DbSet<Db_Attribute> attributes { get; set; }

        public DbSet<Db_Attributes_Category> attribute_categories { get; set; }

        public DbSet<Db_Commerce> commerces { get; set; }

        public DbSet<Db_Commerce_Attribute> commerce_attributes { get; set; }

        public DbSet<Db_Commerce_Type> commerce_types { get; set; }

        public DbSet<Db_Commerce_Image> commerce_images { get; set; }

        public DbSet<Db_Identification_Type> identification_types { get; set; }

        public DbSet<Db_Priority_level> priority_levels { get; set; }

        public DbSet<Db_Advertising_address> advertisings_address { get; set; }


        public DbSet<Db_City> cities { get; set; }
        public DbSet<Db_State> states { get; set; }
        public DbSet<Db_Country> countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Db_Address>()
                .HasKey(a => new { a.add_ref_id, a.add_ref_type, a.add_item });

            modelBuilder.Entity<Db_Advertising_Image>()
                .HasKey(a => new { a.adi_adv_id, a.adi_item });

            modelBuilder.Entity<Db_Advertising_Time_Set>()
                .HasKey(a => new { a.ats_adv_id, a.ats_day });

            modelBuilder.Entity<Db_Commerce_Attribute>()
                .HasKey(a => new { a.coa_com_id, a.coa_atr_id });

            modelBuilder.Entity<Db_Commerce_Image>()
                .HasKey(a => new { a.coi_com_id, a.coi_item });

            modelBuilder.Entity<Db_Advertising_Attribute>()
                .HasKey(a => new { a.ada_adv_id, a.ada_atr_cod });

            modelBuilder.Entity<Db_Advertising_address>()
                .HasKey(a => new { a.aad_adv_id, a.aad_adv_com_id, a.aad_add_item });

            base.OnModelCreating(modelBuilder);
        }
    }
}