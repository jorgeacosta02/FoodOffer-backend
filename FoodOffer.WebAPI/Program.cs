using FoodOffer.AppServices;
using FoodOffer.Infrastructure;
using FoodOffer.Model.Repositories;
using FoodOffer.Model.Services;
using FoodOffer.Repository;
using FoodOffer.WebAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySQL(mySqlConnection));

builder.Services.AddScoped<IDbConecction>(_ =>
{
    return new MySqlConnectionProvider(mySqlConnection);
});

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
builder.Services.AddScoped<IUserService, UserService>(provider =>
{
    var repository = provider.GetRequiredService<IUserRepository>();
    return new UserService(repository);
});

builder.Services.AddScoped<IAdvertisingService, AdvertisingService>(provider =>
{
    var repository = provider.GetRequiredService<IUserRepository>();
    return new AdvertisingService(repository);
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
