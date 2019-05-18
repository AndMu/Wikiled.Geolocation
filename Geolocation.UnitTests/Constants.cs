using Wikiled.Geolocation;

namespace Geolocation.UnitTests
{
    public static class Constants
    {
        public static class Coordinates
        {
            public static Coordinate LatitudeAboveMaximum => new Coordinate {Latitude = 97.0675918, Longitude = -118.3977091};

            public static Coordinate LatitudeBelowMinimum => new Coordinate {Latitude = -91.0675918, Longitude = -118.3977091};

            public static Coordinate LatitudeEqualToMaximum => new Coordinate {Latitude = 90.0000, Longitude = -118.3977091};

            public static Coordinate LatitudeEqualToMinimum => new Coordinate {Latitude = -90.000, Longitude = -118.3977091};

            public static Coordinate LongitudeAboveMaximum => new Coordinate {Latitude = 34.0675918, Longitude = -187.3977091};

            public static Coordinate LongitudeBelowMinumum => new Coordinate {Latitude = 34.0675918, Longitude = -197.3977091};

            public static Coordinate LongitudeEqualToMaximum => new Coordinate {Latitude = 34.0675918, Longitude = 180.0000};

            public static Coordinate LongitudeEqualToMinimum => new Coordinate {Latitude = 34.0675918, Longitude = -180.0000};

            public static Coordinate ValidCoordinate => new Coordinate {Latitude = 34.0675918, Longitude = -118.3977091};

            public static Coordinate ValidDestinationCoordinate => new Coordinate {Latitude = 35.076234, Longitude = -118.9078687};
        }
    }
}
