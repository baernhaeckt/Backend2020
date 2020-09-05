namespace Backend.Core.Features.UserManagement.Responses
{
    public class UserProfileResponse
    {
        public UserProfileResponse(string displayName, string email, in double locationLatitude, in double locationLongitude, string locationCity, string locationStreet, string locationPostalCode)
        {
            DisplayName = displayName;
            Email = email;
            Latitude = locationLatitude;
            Longitude = locationLongitude;
            City = locationCity;
            Street = locationStreet;
            PostalCode = locationPostalCode;
        }

        public string DisplayName { get; }

        public string Email { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string PostalCode { get; }

        public string City { get; }

        public string Street { get; }
    }
}