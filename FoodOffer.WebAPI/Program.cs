using AutoMapper;
using FoodOffer.AppServices;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using FoodOffer.Repository;
using FoodOffer.Repository.Mapper;
using FoodOffer.WebAPI;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

// Se Crea el objeto WebApplicationBuilder
var builder = WebApplication.CreateBuilder(args);

// Se obtiene la cadena de la base de daos de la configuración de aplicación en el contenedor de servicios.
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Se registra el contexto de la base de datos en el contenedor de servicios.
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySQL(mySqlConnection));

builder.Services.AddAutoMapper(config => {
    config.AddProfile<CustomMapper>();
},
Assembly.GetExecutingAssembly());

// Se agregan elementos al contenedor de servicios.

// Se registra la interfaz IDbConnection y su implementación MySqlConnectionProvider en el contenedor de servicios.
// Esta implementación se utiliza para proporcionar una instancia de conexión a la base de datos MySQL.
builder.Services.AddScoped<IDbConecction>(_ =>
{
    return new MySqlConnectionProvider(mySqlConnection);
});

// Se registra la interfaz IUserRepository y su implementación UserRepository en el contenedor de servicios.
// Esta implementación se encarga de interactuar con la base de datos para realizar operaciones relacionadas con los usuarios.
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


//Services registration
// Se registra la interfaz IUserService y su implementación UserService en el contenedor de servicios.
// Esta implementación se utiliza para proporcionar funcionalidades relacionadas con los usuarios.
builder.Services.AddScoped<IUserService, UserService>(provider =>
{
    var userRepository = provider.GetRequiredService<IUserRepository>();
    var loginRepository = provider.GetRequiredService<ILoginRepository>();
    return new UserService(userRepository, loginRepository);
});


// Se registra la interfaz IAdvertisingService y su implementación AdvertisingService en el contenedor de servicios.
// Esta implementación se utiliza para proporcionar funcionalidades relacionadas con la publicidad.
builder.Services.AddScoped<IAdvertisingService, AdvertisingService>(provider =>
{
    var repository = provider.GetRequiredService<IUserRepository>();
    return new AdvertisingService(repository);
});


// Se agregan los controladores MVC al contenedor de servicios.
// Esto permite que los controladores estén disponibles para manejar las solicitudes HTTP entrantes.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
