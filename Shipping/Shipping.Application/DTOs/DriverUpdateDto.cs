namespace Shipping.Application.DTOs
{
    public class DriverUpdateDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string License { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
}
