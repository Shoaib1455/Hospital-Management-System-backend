using IdentityService.DTOs;
using IdentityService.Helpers;
using IdentityService.Interfaces;
using IdentityService.Models;

namespace IdentityService.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenGenerator _jwt;

        public AuthService(
            IUserRepository repository,
            IJwtTokenGenerator jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            if (await _repository.EmailExistsAsync(dto.Email))
                throw new Exception("Email already exists.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            await _repository.AddAsync(user);
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);

            if (user == null)
                return null;

            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!valid)
                return null;

            var token = _jwt.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2),
                User = new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }

        public async Task<UserDto?> GetUserAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
