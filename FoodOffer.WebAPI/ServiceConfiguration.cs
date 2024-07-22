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
        string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySQL(mySqlConnection));

        builder.Services.AddAutoMapper(config => {
            config.AddProfile<CustomMapper>();
        }, Assembly.GetExecutingAssembly());


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

        builder.Services.AddScoped<IAdvertisingRepository, AdvertisingRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new AdvertisingRepository(connection, context, mapper);
        });

        builder.Services.AddScoped<IImagesRepository, ImagesRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new ImagesRepository(connection, context, mapper);
        });

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(provider =>
        {
            var connection = provider.GetRequiredService<IDbConecction>();
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var mapper = provider.GetRequiredService<IMapper>();
            return new CategoryRepository(connection, context, mapper);
        });


        #endregion

        #region Services


        builder.Services.AddScoped<IAdvertisingService, AdvertisingService>(provider =>
        {
            var advertisingRepository = provider.GetRequiredService<IAdvertisingRepository>();
            var imagesRepository = provider.GetRequiredService<IImagesRepository>();
            var s3 = provider.GetRequiredService<AmazonS3Service>();

            return new AdvertisingService(advertisingRepository, imagesRepository, s3);
        });

        builder.Services.AddScoped<IUserService, UserService>(provider =>
        {
            var userRepository = provider.GetRequiredService<IUserRepository>();
            var loginRepository = provider.GetRequiredService<ILoginRepository>();
            return new UserService(userRepository, loginRepository);
        });

        builder.Services.AddScoped<ICategoryService, CategoryService>(provider =>
        {
            var categoryRepository = provider.GetRequiredService<ICategoryRepository>();

            return new CategoryService(categoryRepository);
        });

        #endregion

    }
}


