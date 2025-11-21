using Microsoft.EntityFrameworkCore;
using backend_dotnet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS to allow the Angular dev server (supports multiple ports for development)
var allowAngularDev = "AllowAngularDev";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAngularDev,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:65443", "http://127.0.0.1:4200", "http://127.0.0.1:65443")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// PostgreSQL connection (configured in appsettings.json)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger in Development and allow direct Swagger UI access (useful for local testing)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Also enable Swagger in Production for local development (remove this for production deployment)
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowAngularDev);

app.UseAuthorization();

app.MapControllers();

app.Run();
