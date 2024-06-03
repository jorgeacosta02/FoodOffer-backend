    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using System.Reflection;
    using FoodOffer.AppServices;
    using FoodOffer.Infrastructure;
    using FoodOffer.Model.Repositories;
    using FoodOffer.Model.Services;
    using FoodOffer.Repository.Mapper;
    using FoodOffer.Repository;
using FoodOffer.Model.Models;

public static class ServiceConfiguration
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Se obtiene la cadena de la base de daos de la configuración de aplicación en el contenedor de servicios.
        string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        // Se registra el contexto de la base de datos en el contenedor de servicios.
        builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySQL(mySqlConnection));

        builder.Services.AddAutoMapper(config => {
            config.AddProfile<CustomMapper>();
        }, Assembly.GetExecutingAssembly());

        // Se registra la interfaz IDbConnection y su implementación MySqlConnectionProvider en el contenedor de servicios.
        // Esta implementación se utiliza para proporcionar una instancia de conexión a la base de datos MySQL.
        builder.Services.AddScoped<IDbConecction>(_ =>
        {
            return new MySqlConnectionProvider(mySqlConnection);
        });

        builder.Services.Configure<AWSOptions>(builder.Configuration.GetSection("AWS"));

        builder.Services.AddScoped<AmazonS3Service>();

        #region Repositories
        // Se registran los repositorios.
        builder.Services.AddScoped<IUserRepository, UserRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new UserRepository(connection, context, mapper);
        });

        builder.Services.AddScoped<ILoginRepository, LoginRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new LoginRepository(connection, context, mapper);
        });

        builder.Services.AddScoped<IAddressRepository, AddressRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new AddressRepository(connection, context, mapper);
        });

        builder.Services.AddScoped<IAdvertisingRepository, AdvertisingRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new AdvertisingRepository(connection, context, mapper);
        });

        builder.Services.AddScoped<IAttributeRepository, AttributesRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new AttributesRepository(connection, context, mapper);
        });

        #endregion

        #region Services

        // Se registran los servicios.
        builder.Services.AddScoped<IUserService, UserService>(provider =>
        {
            var userRepository = provider.GetRequiredService<IUserRepository>();
            var loginRepository = provider.GetRequiredService<ILoginRepository>();
            var addressRepository = provider.GetRequiredService<IAddressRepository>();
            return new UserService(userRepository, loginRepository, addressRepository);
        });

        builder.Services.AddScoped<IAdvertisingService, AdvertisingService>(provider =>
        {
            var repository = provider.GetRequiredService<IAdvertisingRepository>();
            var amazon = provider.GetRequiredService<AmazonS3Service>();
            return new AdvertisingService(repository, amazon);
        });

        builder.Services.AddScoped<IAttributeService, AttributeService>(provider =>
        {
            var repository = provider.GetRequiredService<IAttributeRepository>();
            return new AttributeService(repository);
        });

        #endregion

    }
}


