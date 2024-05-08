using FoodOffer.AppServices;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using FoodOffer.Repository;
using FoodOffer.WebAPI;
using Microsoft.EntityFrameworkCore;

// Se Crea el objeto WebApplicationBuilder
var builder = WebApplication.CreateBuilder(args);

// Se obtiene la cadena de la base de daos de la configuraci�n de aplicaci�n en el contenedor de servicios.
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Se registra el contexto de la base de datos en el contenedor de servicios.
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySQL(mySqlConnection));


// Se agregan elementos al contenedor de servicios.

// Se registra la interfaz IDbConnection y su implementaci�n MySqlConnectionProvider en el contenedor de servicios. Esta implementaci�n se utiliza para proporcionar una instancia de conexi�n a la base de datos MySQL.
builder.Services.AddScoped<IDbConecction>(_ =>
{
    return new MySqlConnectionProvider(mySqlConnection);
});

// Se registra la interfaz IUserRepository y su implementaci�n UserRepository en el contenedor de servicios. Esta implementaci�n se encarga de interactuar con la base de datos para realizar operaciones relacionadas con los usuarios.
builder.Services.AddScoped<IUserRepository, UserRepository>(provider =>
{
    var connection = provider.GetRequiredService<IDbConecction>();
    var context = provider.GetRequiredService<ApplicationDbContext>();
    return new UserRepository(connection, context);
});


//Repositories registration
//builder.Services.AddScoped<IUserRepository, UserRepository>(provider =>
//{
//    var context = provider.GetRequiredService<ApplicationDbContext>();
//return new UserRepository(context);
//});


//Services registration
// Se registra la interfaz IUserService y su implementaci�n UserService en el contenedor de servicios. Esta implementaci�n se utiliza para proporcionar funcionalidades relacionadas con los usuarios.
builder.Services.AddScoped<IUserService, UserService>(provider =>
{
    var repository = provider.GetRequiredService<IUserRepository>();
    return new UserService(repository);
});


// Se registra la interfaz IAdvertisingService y su implementaci�n AdvertisingService en el contenedor de servicios. Esta implementaci�n se utiliza para proporcionar funcionalidades relacionadas con la publicidad.
builder.Services.AddScoped<IAdvertisingService, AdvertisingService>(provider =>
{
    var repository = provider.GetRequiredService<IUserRepository>();
    return new AdvertisingService(repository);
});


// Se agregan los controladores MVC al contenedor de servicios. Esto permite que los controladores est�n disponibles para manejar las solicitudes HTTP entrantes.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
