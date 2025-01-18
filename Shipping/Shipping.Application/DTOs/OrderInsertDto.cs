namespace Shipping.Application.DTOs
{
    public class OrderInsertDto
    {
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
