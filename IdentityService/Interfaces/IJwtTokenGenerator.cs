using IdentityService.Models;

namespace IdentityService.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
