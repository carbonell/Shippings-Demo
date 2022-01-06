using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ShipmentsApi.Models;
using Xunit;

namespace ShipmentsApi.Tests
{
    public class PackagesControllerTests
    {
        public PackagesControllerTests()
        { }

        private string _baseUrl = "api/packages";

        [Fact]
        public virtual async Task Test_Get_Package()
        {
            // ARRANGE
            HttpClient client = GetHttpClient();
            Package model = await GetPackageFromContext();

            // ACT

            var response = await client.GetAsync($"{_baseUrl}/{model.Id}");

            // ASSERT
            await AssertNotNull<Package>(response);
        }

        private static HttpClient GetHttpClient()
        {
            var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                });
            });

            var client = application.CreateClient();
            return client;
        }

        public static async Task AssertNotNull<TModel>(HttpResponseMessage response) where TModel : class
        {
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(result);
            Assert.NotNull(model);
        }

        // FIX THIS PLEASE!
        private Task<Package> GetPackageFromContext()
        {
            return Task.FromResult(new Package { Id = 1 });
        }
    }
}