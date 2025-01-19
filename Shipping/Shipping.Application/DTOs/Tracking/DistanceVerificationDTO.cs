namespace Shipping.Application.DTOs.Tracking
{
    public class DistanceVerificationDTO
    {
        public double OriginLatitude { get; set; }
        public double OriginLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
        public double SpeedKmH { get; set; } = 40;
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
