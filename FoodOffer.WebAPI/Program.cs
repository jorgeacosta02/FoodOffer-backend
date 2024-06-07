
var builder = WebApplication.CreateBuilder(args);

ServiceConfiguration.ConfigureServices(builder);

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {

        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();

    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
