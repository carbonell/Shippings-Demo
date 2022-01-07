namespace ShipmentsApi.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Guid ShipmentId { get; set; }
        public Shipment? Shipment { get; set; }
    }
}