using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Backend.Core.Features.UserManagement.Requests;
using Backend.Models;
using Newtonsoft.Json;

namespace Backend.Tests.Utilities
{
    public static class HttpClientExtensionss
    {
        public static async Task SignIn(this HttpClient client, string email, string password)
        {
            var url = new Uri("api/users/Login", UriKind.Relative);
            StringContent content = new UserLoginRequest { Email = email, Password = password }.ToStringContent();
            HttpResponseMessage responseWithJwt = await client.PostAsync(url, content);
            responseWithJwt.EnsureSuccessStatusCode();
            string jwt = await responseWithJwt.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<UserLoginResponse>(jwt);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
        }

        public static async Task<string> CreateUserAndSignIn(this HttpClient client)
        {
            string email = "marc.sallin@outlook.com";
            var url = new Uri("api/users/Register", UriKind.Relative);
            StringContent content = new RegisterUserRequest { Email = email, Password = "test" }.ToStringContent();
            HttpResponseMessage response = await client.PostAsync(url, content);
            UserLoginResponse userLoginResponse = await response.OnSuccessDeserialize<UserLoginResponse>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userLoginResponse.Token);
            return email;
        }
    }
}