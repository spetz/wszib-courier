namespace Courier.Core.Domain
{
    public class Address
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public string Location { get; }

        private Address(double latitude, double longitude, string location)
        {
            Latitude = latitude;
            Longitude = longitude;
            Location = location;
        }

        public static Address Create(double latitude, double longitude, string location)
            => new Address(latitude, longitude, location);
    }
}