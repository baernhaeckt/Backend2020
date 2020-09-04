using Microsoft.IdentityModel.Tokens;

namespace Backend.Infrastructure.Abstraction.Security
{
    public interface ISecurityKeyProvider
    {
        SymmetricSecurityKey GetSecurityKey();
    }
}