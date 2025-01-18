namespace Shipping.Application.DTOs
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string License { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
}
