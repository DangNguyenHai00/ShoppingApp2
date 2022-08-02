using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp2.Configuration;
using ShoppingApp2.Data.DTO.Request;
using ShoppingApp2.Data.DTO.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace ShoppingApp2.Service
{
    public class AuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthenticationService(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _roleManager = roleManager;
        }
        public async Task<RegistrationResponse> CreateUser(UserRegistrationRequest dto)
        {
            //Need Model.IsValid from controller
            //Controller return ok or bad request
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return new RegistrationResponse()
                {
                    Errors = new List<string>() { "Email has already been used." },
                    Success = false,
                    Message = "Try another email adress."
                };
            }
            var new_user = new IdentityUser() { Email = dto.Email, UserName = dto.Email };
            var isCreated = await _userManager.CreateAsync(new_user, dto.Password);
            if (isCreated.Succeeded)
            {
                var key = await _userManager.AddToRoleAsync(new_user, "Customer");
                return (new RegistrationResponse
                {
                    Success = true,
                    Message = "Your account has been registered successfully."
                });
            }
            else
            {
                return new RegistrationResponse()
                {
                    Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                    Success = false,
                    Message = "Please retry again later or find a way to contact us."
                };
            }
        }

        public async Task<LoginResponse> Login(UserLoginRequest user)
        {
            //Need Model.IsValid from controller
            //Controller return ok or bad request
            var existing_user = await _userManager.FindByNameAsync(user.UserName);
            if (existing_user == null)
            {
                return new LoginResponse()
                {
                    Errors = new List<string>()
                        {
                        "This email address has not been registed yet.",
                        },
                    Success = false
                };
            }
            var isCorrect = await _userManager.CheckPasswordAsync(existing_user, user.Password);
            if (!isCorrect)
            {
                return new LoginResponse()
                {
                    Errors = new List<string>()
                        {
                            "You are entering the wrong password.",
                        },
                    Success = false
                };
            }

            var jwtToken = await GenerateJwtToken(existing_user);

            return new LoginResponse()
            {
                Success = true,
                Token = jwtToken,
            };
        }

        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var role = await _userManager.GetRolesAsync(user);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Role,role.First()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public async Task<string>  AddRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (!roleExist) // checks on the role exist status
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

                // We need to check if the role has been added successfully
                if (roleResult.Succeeded)
                {
                    return String.Format("The role {0} has been added successfully",name);
                }
                else
                {
                    return String.Format($"The role {0} has not been added", name);
                }

            }
            return String.Format("Role already exist");
        }
    }
}
