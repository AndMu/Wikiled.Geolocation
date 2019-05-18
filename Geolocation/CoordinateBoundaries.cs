using System;

namespace Wikiled.Geolocation
{
    /// <summary>
    ///     Calculates the upper, lower, left and right coordinate boundaries based on an origin point and a distance.
    /// </summary>
    public class CoordinateBoundaries
    {
        private double distance;

        private DistanceUnit distanceUnit;

        private double latitude;

        private readonly double latitudeDistanceInKilometers = 111.045;

        private readonly int latitudeDistanceInMeters = 111045;

        private readonly int latitudeDistanceInMiles = 69;

        private readonly int latitudeDistanceInNauticalMiles = 60;

        private double longitude;

        /// <summary>
        ///     Creates a new CoordinateBoundary object
        /// </summary>
        public CoordinateBoundaries()
        {
        }

        /// <summary>
        ///     Creates a new CoordinateBoundary object
        /// </summary>
        /// <param name="originCoordinate">A <see cref="Coordinate" /> object representing the origin location</param>
        /// <param name="distance">The distance from the origin point in statute miles</param>
        /// <param name="distanceUnit">The unit of distance</param>
        public CoordinateBoundaries(Coordinate originCoordinate, double distance, DistanceUnit distanceUnit = DistanceUnit.Miles)
            : this(originCoordinate.Latitude, originCoordinate.Longitude, distance, distanceUnit)
        {
        }

        /// <summary>
        ///     Creates a new CoordinateBoundary object
        /// </summary>
        /// <param name="latitude">The origin point latitude in decimal notation</param>
        /// <param name="longitude">The origin point longitude in decimal notation</param>
        /// <param name="distance">The distance from the origin point in statute miles</param>
        /// <param name="distanceUnit">The unit of distance</param>
        public CoordinateBoundaries(double latitude, double longitude, double distance, DistanceUnit distanceUnit = DistanceUnit.Miles)
        {
            if (!CoordinateValidator.Validate(latitude, longitude))
            {
                throw new ArgumentException("Invalid coordinates supplied.");
            }

            this.latitude = latitude;
            this.longitude = longitude;
            this.distance = distance;
            this.distanceUnit = distanceUnit;

            Calculate();
        }

        /// <summary>
        ///     The distance in statue miles from the origin point
        /// </summary>
        public double Distance
        {
            get => distance;
            set
            {
                distance = value;
                Calculate();
            }
        }

        /// <summary>
        ///     The distance unit.
        /// </summary>
        public DistanceUnit DistanceUnit
        {
            get => distanceUnit;
            set
            {
                distanceUnit = value;
                Calculate();
            }
        }

        /// <summary>
        ///     The origin point latitude in decimal notation
        /// </summary>
        public double Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                Calculate();
            }
        }

        /// <summary>
        ///     The origin point longitude in decimal notation
        /// </summary>
        public double Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                Calculate();
            }
        }

        /// <summary>
        ///     The lower boundary latitude point in decimal notation
        /// </summary>
        public double MaxLatitude { get; private set; }

        /// <summary>
        ///     The right boundary longitude point in decimal notation
        /// </summary>
        public double MaxLongitude { get; private set; }

        /// <summary>
        ///     The upper boundary latitude point in decimal notation
        /// </summary>
        public double MinLatitude { get; private set; }

        /// <summary>
        ///     The left boundary longitude point in decimal notation
        /// </summary>
        public double MinLongitude { get; private set; }

        private void Calculate()
        {
            if (!CoordinateValidator.Validate(Latitude, Longitude))
            {
                throw new ArgumentException("Invalid coordinates supplied.");
            }

            double divisor = GetDivisor();

            double latitudeConversionFactor = Distance / divisor;
            double longitudeConversionFactor = Distance / divisor / Math.Abs(Math.Cos(Latitude.ToRadian()));

            MinLatitude = Latitude - latitudeConversionFactor;
            MaxLatitude = Latitude + latitudeConversionFactor;

            MinLongitude = Longitude - longitudeConversionFactor;
            MaxLongitude = Longitude + longitudeConversionFactor;

            // Adjust for passing over coordinate boundaries
            if (MinLatitude < -90)
            {
                MinLatitude = 90 - (-90 - MinLatitude);
            }

            if (MaxLatitude > 90)
            {
                MaxLatitude = -90 + (MaxLatitude - 90);
            }

            if (MinLongitude < -180)
            {
                MinLongitude = 180 - (-180 - MinLongitude);
            }

            if (MaxLongitude > 180)
            {
                MaxLongitude = -180 + (MaxLongitude - 180);
            }
        }

        private double GetDivisor()
        {
            switch (distanceUnit)
            {
                case DistanceUnit.NauticalMiles:
                    return latitudeDistanceInNauticalMiles;
                case DistanceUnit.Kilometers:
                    return latitudeDistanceInKilometers;
                case DistanceUnit.Meters:
                    return latitudeDistanceInMeters;
                default:
                    return latitudeDistanceInMiles;
            }
        }
    }
}
