using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ShoppingApp2.Service;
using ShoppingApp2.Data.Models;
using ShoppingApp2.Data.DTO.Request;
//using ShoppingApp2.Data.DTO.Response;

namespace ShoppingApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("Customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _adminService.GetCustomers();
            return Ok(customers);
        }

        [HttpGet]
        [Route("Admins")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAdmins();
            return Ok(admins);
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem(Item item)
        {
            bool key = await _adminService.AddNewItem(item);
            if (key == true)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPost]
        [Route("NewAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> NewAdmin(AdminRegisterRequest dto)
        {
            var response = await _adminService.CreateAdmin(dto);
            return Ok(response);
        }

        [HttpPost]
        [Route("Restock")]
        public async Task<IActionResult> RestockItem(RestockRequest dto)
        {
            if(dto.number<=0)
            {
                return BadRequest("Restock quantity must bigger then zero.");
            }
            var item = await _adminService.Restock(dto.Id, dto.number);
            if (item != null)
            {
                return Ok(item);
            }
            else
                return BadRequest("ItemId does not exist.");
        }
    }
}
