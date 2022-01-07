using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ShipmentsApi.Models;
using Xunit;

namespace ShipmentsApi.Tests
{
    public class ShipmentsControllerTests
    {
        public ShipmentsControllerTests()
        { }

        private string _baseUrl = "api/Shipments";

        [Fact]
        public virtual async Task Can_Get_Shipment()
        {
            // ARRANGE
            HttpClient client = GetHttpClient();
            var shipmentId = new Guid("3cb5143a-e543-4eac-9700-1929281ad912");

            // ACT

            var response = await client.GetAsync($"{_baseUrl}/{shipmentId}");

            // ASSERT
            var shipment = await Parse<Shipment>(response);
            Assert.NotNull(shipment);
        }

        [Fact]
        public virtual async Task Can_ProcessShipment()
        {
            // ARRANGE
            HttpClient client = GetHttpClient();
            var shipmentId = new Guid("3cb5143a-e543-4eac-9700-1929281ad912");

            // ACT

            var response = await client.PostAsync($"{_baseUrl}/ProcessDelivery/{shipmentId}", null);

            // ASSERT
            var shipment = await Parse<Shipment>(response);
            Assert.NotNull(shipment);
            Assert.Equal(ShipmentStatus.Delivered, shipment?.Status);
            Assert.NotNull(shipment?.DeliveryDate);
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

        public static async Task<TModel?> Parse<TModel>(HttpResponseMessage response) where TModel : class
        {
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(result);
        }

    }
}