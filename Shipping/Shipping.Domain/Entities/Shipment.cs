using NetTopologySuite.Geometries;

namespace Shipping.Domain.Entities
{
    public class Shipment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid ShipmentTypeId { get; set; }
        public Point OriginLocation { get; set; }
        public Point DestinationLocation { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; }
        public decimal ShippingCost { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string TransportedGoods { get; set; }

        public ShipmentType ShipmentType { get; set; }
        public Order Order { get; set; }
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<ShipmentTracking> ShipmentTrackings { get; set; }
        public Payment Payment { get; set; }
    }
}
