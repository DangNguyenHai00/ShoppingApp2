using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp2.Service;
using ShoppingApp2.Data.DTO.Request;
using ShoppingApp2.Data.DTO.Response;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationService _authenticationService;
        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _authenticationService.CreateUser(dto);
                return Ok(response);
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                { "Invalid payload."},
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest dto)
        {
            if (ModelState.IsValid)
            {
                var respone = await _authenticationService.Login(dto);
                return Ok(respone);
            }
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload.",
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] string name)
        {
            string result = await _authenticationService.AddRole(name);
            return Ok(result);
        }
    }
}
