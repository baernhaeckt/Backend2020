namespace Backend.Core.Entities
{
    public class Location
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string PostalCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;
    }
}