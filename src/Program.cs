using Microsoft.EntityFrameworkCore;
using NGroot;
using ShipmentsApi;
using ShipmentsApi.Models;
using ShipmentsApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configure DB
var connectionStringsSection = builder.Configuration.GetSection(nameof(ConnectionStrings));
var connectionString = connectionStringsSection.Get<ConnectionStrings>()?.ShipmentsDb ?? "";


builder.Services.AddDbContext<ShipmentsContext>(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.UseSqlServer(connectionString));

// Configure Settings
var initialDataSettingsSection = builder.Configuration.GetSection("InitialDataSettings");
builder.Services.Configure<NgrootSettings<InitialData>>(initialDataSettingsSection);
// Configure Loader
builder.Services.AddScoped<IPackagesLoader, PackagesLoader>();

// Swagger
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

public partial class Program { }