using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Backend.Infrastructure.Abstraction.Security;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = Backend.Infrastructure.Abstraction.Security.ClaimTypes;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Backend.Infrastructure.Security
{
    public class JwtSecurityTokenFactory : ISecurityTokenFactory
    {
        private const int TokenExpirationInMinutes = 60 * 24; // Set this to 15min after refresh is implemented.

        private readonly ISecurityKeyProvider _securityKeyProvider;

        public JwtSecurityTokenFactory(ISecurityKeyProvider securityKeyProvider) => _securityKeyProvider = securityKeyProvider;

        public string Create(Guid id, string subject, IEnumerable<string> roles)
        {
            var now = DateTime.Now;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserId, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, subject),
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(CultureInfo.CurrentCulture), ClaimValueTypes.Integer64)
            };

            claims.AddRange(roles.Select(r => new Claim(System.Security.Claims.ClaimTypes.Role, r)));

            SymmetricSecurityKey signingKey = _securityKeyProvider.GetSecurityKey();
            var securityToken = new JwtSecurityToken(
                "Tuuri",
                "Tuuri",
                claims,
                now,
                now.AddMinutes(TokenExpirationInMinutes),
                new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}