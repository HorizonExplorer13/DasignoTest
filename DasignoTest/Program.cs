using DasignoTest.AuxModels.AuxResponseModels;
using DasignoTest.AuxServices;
using DasignoTest.AuxServices.Middleware;
using DasignoTest.DBContext;
using DasignoTest.Entitys.Users;
using DasignoTest.Services.UserServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddControllers().AddJsonOptions(
    options => {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(Connection));
builder.Services.AddControllers();
builder.Services.AddScoped<SeedMigrationsNData>();
builder.Services.AddScoped<IUserService, UserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
var lifeTime = app.Services.GetRequiredService<IHostApplicationLifetime>();
app.UseMiddleware<AutomateMigrations>();
lifeTime.ApplicationStarted.Register(async () =>
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            // Obtener el servicio SeedMigrationsNData
            var seedService = services.GetRequiredService<SeedMigrationsNData>();

            // Ejecutar la semilla de datos y migraciones de manera síncrona
            await seedService.SeedDataAsync();
        }
        catch (Exception ex)
        {
            // Manejar errores
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "there was an error seedding the data");
        }
    }
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
