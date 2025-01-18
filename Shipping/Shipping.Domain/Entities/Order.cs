namespace Shipping.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public ICollection<Shipment> Shipments { get; set; }
    }
}
