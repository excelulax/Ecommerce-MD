namespace Shipping.Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string License { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }

        public ICollection<Shipment> Shipments { get; set; }
        public ICollection<DriverVehicle> DriverVehicles { get; set; }
    }
}
