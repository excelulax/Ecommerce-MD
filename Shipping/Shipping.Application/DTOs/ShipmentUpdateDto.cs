namespace Shipping.Application.DTOs
{
    public class ShipmentUpdateDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public double OriginLocationLatitude { get; set; }
        public double OriginLocationLongitude { get; set; }
        public double DestinationLocationLatitude { get; set; }
        public double DestinationLocationLongitude { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
