using IdentityService.Models;

namespace IdentityService.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(Guid id);

        Task<bool> EmailExistsAsync(string email);
    }
}
