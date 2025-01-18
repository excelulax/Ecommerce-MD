namespace Shipping.Domain.Entities
{
    public class DriverVehicle
    {
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? RevokedDate { get; set; }
        public string AssignmentStatus { get; set; }
        public string Notes { get; set; }

        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
