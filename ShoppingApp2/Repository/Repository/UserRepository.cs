using Microsoft.AspNetCore.Identity;
using ShoppingApp2.Configuration;
using ShoppingApp2.Data.DTO.Request;
using ShoppingApp2.Data.DTO.Response;

namespace ShoppingApp2.Repository.Repository
{
    public class UserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public UserRepository(UserManager<IdentityUser> userManager, JwtConfig jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig;
        }

        public async Task<bool> CreateUser(UserRegistrationRequest dto)
        {
            var new_user = new IdentityUser() { Email = dto.Email, UserName = dto.Email };
            var isCreated = await _userManager.CreateAsync(new_user, dto.Password);
            if (isCreated.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
