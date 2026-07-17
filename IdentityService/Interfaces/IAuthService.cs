using IdentityService.DTOs;

namespace IdentityService.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);

        Task<LoginResponseDto?> LoginAsync(LoginDto dto);

        Task<UserDto?> GetUserAsync(Guid id);
    }
}
