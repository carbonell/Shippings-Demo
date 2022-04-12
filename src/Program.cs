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
// Configure Loader
builder.Services.ConfigureNGroot<InitialData>(settings =>
{
    settings.InitialDataFolderRelativePath = "InitialData/Data";
    settings.SeedTestData = true;
    settings.PathConfiguration = new List<BaseDataSettings<InitialData>>
    {
        new BaseDataSettings<InitialData>{ Identifier = InitialData.Packages, RelativePath = "Packages.json"},
        new BaseDataSettings<InitialData>{ Identifier = InitialData.Shipments, RelativePath = "Shipments.json"}
    };
}, typeof(Program).Assembly);

// Swagger
builder.Services.AddSwaggerGen();
var provider = builder.Services.BuildServiceProvider();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await provider.LoadData<InitialData>(new Type[]
    {
        typeof(ShipmentsLoader),
        typeof(PackagesLoader),
    }, contentRootPath: app.Environment.ContentRootPath);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }