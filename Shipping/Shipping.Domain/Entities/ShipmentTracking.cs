using NetTopologySuite.Geometries;

namespace Shipping.Domain.Entities
{
    public class ShipmentTracking : BaseEntity
    {
        public Guid ShipmentId { get; set; }
        public Point CurrentLocation { get; set; }
        public DateTime TrackedTime { get; set; }
        public string Status { get; set; }
        public decimal EstimatedDistance { get; set; }
        public int EstimatedMinutes { get; set; }

        public Shipment Shipment { get; set; }
    }
}
