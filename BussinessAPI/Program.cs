using BussinessAPI;
using BussinessAPI.Controllers;
using BussinessAPI.DAL;
using BussinessAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<currencydbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConnStr");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//Caching
builder.Services.AddDbContext<CachingDataDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConnStr");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<ICurrencyServices, CurrencyServices>();
builder.Services.Decorate<ICurrencyServices, CachedCurrencyService>();

builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
app.Run();
