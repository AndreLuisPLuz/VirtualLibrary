using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.DataTransfer.Payload;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Api.Services.Services
{
    public class LoginService: ILoginService
    {
        private UserRepository _userRepository;
        private static readonly PasswordHasher<User> passwordHasher = new();

        public LoginService()
        {
            _userRepository = new UserRepository(new AppDbContext());
        }

        public async Task<Boolean> TryLogin(LoginPayload payload)
        {
            User? user = await _userRepository.FetchOneByEmailAsync(payload.Username);

            if (user == null)
            {
                return false;
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.Password, payload.Password);
            return result.HasFlag(PasswordVerificationResult.Success);
        }
    }
}
