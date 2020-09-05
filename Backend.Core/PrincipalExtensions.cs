using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Backend.Core
{
    public static class PrincipalExtensions
    {
        public static Guid Id(this IPrincipal principal)
        {
            string id = principal.GetValueFromClaim(ClaimTypes.UserId);
            if (id == null)
            {
                throw new InvalidOperationException("No Id claim found.");
            }

            return Guid.Parse(id);
        }

        private static string GetValueFromClaim(this IPrincipal principal, string name)
        {
            var claimsIdentity = principal?.Identity as ClaimsIdentity;
            return claimsIdentity?.Claims.SingleOrDefault(c => string.Equals(c.Type, name, StringComparison.OrdinalIgnoreCase))?.Value;
        }
    }
}