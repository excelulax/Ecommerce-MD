namespace Shipping.Domain.Entities
{
    public class ShipmentType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
    }
}
