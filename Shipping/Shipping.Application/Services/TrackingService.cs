
using Shipping.Application.DTOs.Tracking;

namespace Shipping.Application.Services
{
    public class TrackingService
    {
        private readonly double _centerLatitude;
        private readonly double _centerLongitude;
        private readonly double _maxDistanceInMeters;

        public TrackingService(double centerLatitude = -21.531269, double centerLongitude = -64.734827, double maxDistanceKm = 5)
        {
            _centerLatitude = centerLatitude;
            _centerLongitude = centerLongitude;
            _maxDistanceInMeters = maxDistanceKm * 1000;
        }

        public LocationVerificationResponseDTO VerifyLocation(DistanceVerificationDTO dto)
        {
            var originDistance = GetHaversineDistance(_centerLatitude, _centerLongitude,
                                                    dto.OriginLatitude, dto.OriginLongitude);
            var destinationDistance = GetHaversineDistance(_centerLatitude, _centerLongitude,
                                                         dto.DestinationLatitude, dto.DestinationLongitude);

            var distanceBetweenPoints = GetHaversineDistance(dto.OriginLatitude, dto.OriginLongitude,
                                                            dto.DestinationLatitude, dto.DestinationLongitude);

            var isWithinRange = originDistance <= _maxDistanceInMeters &&
                               destinationDistance <= _maxDistanceInMeters;

            return new LocationVerificationResponseDTO
            {
                DistanceInKm = Math.Round(distanceBetweenPoints / 1000, 2),
                DistanceInMeters = Math.Round(distanceBetweenPoints, 2),
                IsWithinRange = isWithinRange,
                EstimatedArrivalMinutes = Math.Round((distanceBetweenPoints / 1000) / dto.SpeedKmH * 60, 1),
                FormattedDistance = FormatDistance(distanceBetweenPoints),
                Status = DetermineStatus(originDistance, destinationDistance, _maxDistanceInMeters)
            };
        }

        private LocationStatus DetermineStatus(double originDistance, double destinationDistance, double maxDistance)
        {
            if (originDistance <= maxDistance && destinationDistance <= maxDistance)
                return LocationStatus.WithinRange;
            if (originDistance > maxDistance * 2 || destinationDistance > maxDistance * 2)
                return LocationStatus.TooFar;
            return LocationStatus.OutOfRange;
        }

        private string FormatDistance(double meters)
        {
            return meters >= 1000? $"{Math.Round(meters / 1000, 2)} km": $"{Math.Round(meters, 0)} m";
        }

        private double GetHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371e3;
            var φ1 = lat1 * Math.PI / 180;
            var φ2 = lat2 * Math.PI / 180;
            var Δφ = (lat2 - lat1) * Math.PI / 180;
            var Δλ = (lon2 - lon1) * Math.PI / 180;
            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
    }
}
