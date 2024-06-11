
using Kolos2.DbContext;
using Kolos2.Repository;
using Kolos2.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kolokwium2 API", Version = "v1" });
});

// Add DbContext configuration
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Register application services
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kolokwium2 API V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();