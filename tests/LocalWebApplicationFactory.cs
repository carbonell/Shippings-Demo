using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Net.Http;
using Microsoft.Extensions.Hosting;

namespace ShipmentsApi.Tests
{
    public class LocalWebApplicationFixtureFactory<TStartup>
        : IClassFixture<LocalWebApplicationFactory<TStartup>> where TStartup : class
    {
        public readonly LocalWebApplicationFactory<TStartup> _context;
        public LocalWebApplicationFixtureFactory(
            LocalWebApplicationFactory<TStartup> factory
        )
        {
            _context = factory;
        }

        public IServiceProvider GetServiceProvider()
        {
            return _context.Server.Host.Services.CreateScope().ServiceProvider;
        }
    }

    public class LocalWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private HttpClient _client;
        public LocalWebApplicationFactory()
        {
            _client = CreateClient();
        }

        // protected virtual IHostBuilder CreateHostBuilder()
        // {
        //     var hostBuilder = HostFactoryResolver.ResolveHostBuilderFactory<IHostBuilder>(typeof(TStartup).Assembly)?.Invoke(Array.Empty<string>());
        //     if (hostBuilder != null)
        //     {
        //         hostBuilder.UseEnvironment(Environments.Development);
        //     }
        //     return hostBuilder;
        // }
        public IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder().
            AddJsonFile("appsettings.test.json");
            return config.Build();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Configure(builder);
        }

        private void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
             {
                 ConfigureTestServices(services);
             });

            // var app = builder.Build();

        }


        private void ConfigureTestServices(IServiceCollection services)
        {
            var config = InitConfiguration();

            // var settings = SettingsConfiguration.ConfigureSettings(services, config);
            // var integrationSettingsSection = config.GetSection("IntegrationTestsSettings");
            // var integrationSettings = integrationSettingsSection.Get<IntegrationTestsSettings>();
            // IntegrationTestsSettings = integrationSettings;

            // var connectionStringsSection = config.GetSection("ConnectionStrings");
            // var connectionStrings = connectionStringsSection.Get<ConnectionStrings>();
            // ConnectionStrings = connectionStrings;
            // SetupSqlServerContext<MyProjectDb>(services);
        }

        private void SetupSqlServerContext<TContext>(IServiceCollection services)
        where TContext : DbContext
        {
            // services.AddScoped<ITransactionProvider, TransactionProvider>();
            // var dbContextDescriptor = services.SingleOrDefault(
            //     d => d.ServiceType ==
            //         typeof(DbContextOptions<TContext>));

            // if (dbContextDescriptor != null)
            // {
            //     services.Remove(dbContextDescriptor);
            // }

            // services.AddDbContext<TContext>(opts =>
            // opts.UseSqlServer(ConnectionStrings.ApiDb), ServiceLifetime.Transient);
        }






    }
}
