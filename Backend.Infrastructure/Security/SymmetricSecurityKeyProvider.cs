using System.Text;
using Backend.Infrastructure.Abstraction.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Infrastructure.Security
{
    public class SymmetricSecurityKeyProvider : ISecurityKeyProvider
    {
        private readonly IConfiguration _configuration;

        public SymmetricSecurityKeyProvider(IConfiguration configuration) => _configuration = configuration;

        public SymmetricSecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
    }
}