using Microsoft.EntityFrameworkCore;
using ShipmentsApi;
using ShipmentsApi.Models;
using ShipmentsApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configure Services and DB
var connectionStringsSection = builder.Configuration.GetSection(nameof(ConnectionStrings));
var connectionString = connectionStringsSection.Get<ConnectionStrings>()?.ShipmentsDb ?? "";
builder.Services.AddDbContext<ShipmentsContext>(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.UseSqlServer(connectionString));
builder.Services.AddScoped<IPackagesLoader, PackagesLoader>();
builder.Services.AddSwaggerGen();
var provider = builder.Services.BuildServiceProvider();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await provider.ConfigureInitialData(app.Environment);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
