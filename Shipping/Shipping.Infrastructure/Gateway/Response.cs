namespace Shipping.Infrastructure.Gateway
{
    public class Response
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
