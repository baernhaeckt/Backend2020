namespace Backend.Core.Features.UserManagement.Responses
{
    public class UserLoginResponse
    {
        public UserLoginResponse(string token) => Token = token;

        public string Token { get; set; }
    }
}