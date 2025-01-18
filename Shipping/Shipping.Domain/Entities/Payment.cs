namespace Shipping.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid ShipmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }

        public Shipment Shipment { get; set; }
    }
}
