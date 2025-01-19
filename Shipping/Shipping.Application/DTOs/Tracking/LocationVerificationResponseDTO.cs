namespace Shipping.Application.DTOs.Tracking
{
    public class LocationVerificationResponseDTO
    {
        public double DistanceInKm { get; set; }
        public double DistanceInMeters { get; set; }
        public bool IsWithinRange { get; set; }
        public double EstimatedArrivalMinutes { get; set; }
        public string FormattedDistance { get; set; }
        public LocationStatus Status { get; set; }
    }
}
