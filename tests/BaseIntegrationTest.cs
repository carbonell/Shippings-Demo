using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace ShipmentsApi.Tests
{
    public abstract class BaseIntegrationTest<DatabaseContext>
        : LocalWebApplicationFixtureFactory<Startup>
        where DatabaseContext : DbContext
    {
        protected readonly WebApplicationFactory<Startup> _factory;
        protected readonly HttpClient _httpClient;
        protected IServiceProvider _serviceProvider;

        public BaseIntegrationTest(LocalWebApplicationFactory<Startup> factory) : base(factory)
        {
            _factory = ConfigureApplicationFactory(factory);
            _serviceProvider = GetServiceProvider(_factory);
            _httpClient = _factory.CreateClient();
        }

        public virtual WebApplicationFactory<Startup> ConfigureApplicationFactory(LocalWebApplicationFactory<Startup> factory)
        {
            return factory;
        }

        private IServiceProvider GetServiceProvider(WebApplicationFactory<Startup> appFactory)
        {
            appFactory.CreateClient();
            return appFactory.Server.Host.Services.CreateScope().ServiceProvider;
        }

        public DatabaseContext GetDbContext()
        {
            return _serviceProvider.GetRequiredService<DatabaseContext>();
        }

        public async Task RemoveEntityAsync<TModel>(Expression<Func<TModel, bool>> queryExpression) where TModel : class
        {
            using (var context = GetDbContext())
            {
                var model = (await GetModel(queryExpression));
                if (model != null)
                {
                    context.Remove(model);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected async Task AssertEntity<TModel>(Expression<Func<TModel, bool>> queryExpression) where TModel : class
        {
            using (var context = GetDbContext())
            {
                var model = (await GetModel(queryExpression));
                Assert.NotNull(model);
            }
        }

        public async Task<TModel?> GetModel<TModel>(Expression<Func<TModel, bool>> queryExpression) where TModel : class
        {
            using (var context = GetDbContext())
            {
                return await context.Set<TModel>().IgnoreQueryFilters().FirstOrDefaultAsync(queryExpression);
            }
        }

        public async Task<List<TModel>> GetList<TModel>(Expression<Func<TModel, bool>> queryExpression) where TModel : class
        {
            using (var context = GetDbContext())
            {
                return await context.Set<TModel>().IgnoreQueryFilters().Where(queryExpression).ToListAsync();
            }
        }

        public async Task AssertNotNull<TModel>(HttpResponseMessage response) where TModel : class
        {
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(result);
            Assert.NotNull(model);
        }
    }
}