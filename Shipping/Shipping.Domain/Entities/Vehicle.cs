namespace Shipping.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public ICollection<DriverVehicle> DriverVehicles { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
    }
}
