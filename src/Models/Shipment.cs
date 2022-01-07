namespace ShipmentsApi.Models
{
    public class Shipment
    {
        public Shipment(Guid id, string senderName, string senderAddress, DateTime estimatedDeliveryDate)
        {
            Id = id;
            SenderName = senderName;
            SenderAddress = senderAddress;
            EstimatedDeliveryDate = estimatedDeliveryDate;
        }

        public Guid Id { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;

    }

    public enum ShipmentStatus
    {
        None = 0,
        Pending = 1,
        Delivered = 2
    }
}