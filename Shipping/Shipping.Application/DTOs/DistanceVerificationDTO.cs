namespace Shipping.Application.DTOs
{
    public class DistanceVerificationDTO
    {
        public double OriginLatitude { get; set; }
        public double OriginLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
    }
}
