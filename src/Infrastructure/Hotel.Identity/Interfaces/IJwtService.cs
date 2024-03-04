using Microsoft.AspNetCore.Identity;

namespace Hotel.Infrastructure.Identity.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(IdentityUser user, string role);
        //string GenerateRefreshToken();
    }
}
