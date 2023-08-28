using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserApiEFPostgres.Data;
using UserApiEFPostgres.Models;
using UserApiEFPostgres.Repo;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.


builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
