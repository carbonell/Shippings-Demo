using System;
using System.Threading.Tasks;
using ShipmentsApi.Models;
using Xunit;

namespace ShipmentsApi.Tests
{
    public class PackagesControllerTests : BaseIntegrationTest<ShipmentsContext>
    {
        public PackagesControllerTests(LocalWebApplicationFactory<Startup> factory) : base(factory)
        { }

        private string _baseUrl = "api/packages";

        [Fact]
        public virtual async Task Test_Get_Package()
        {
            // ARRANGE
            Package model = await GetPackageFromContext();

            // ACT

            var response = await _httpClient.GetAsync($"{_baseUrl}/{model.Id}");

            // ASSERT
            await AssertNotNull<Package>(response);
        }

        private Task<Package> GetPackageFromContext()
        {
            throw new NotImplementedException();
        }
    }
}