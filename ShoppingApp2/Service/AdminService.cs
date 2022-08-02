using Microsoft.AspNetCore.Identity;
using ShoppingApp2.Data.Models;
using ShoppingApp2.UnitOfWork;
using ShoppingApp2.Data.DTO.Request;
using ShoppingApp2.Data.DTO.Response;

namespace ShoppingApp2.Service
{
    public class AdminService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddNewItem(Item item)
        {
            var new_item = new Item() { ItemType = item.ItemType, Manufacturer = item.Manufacturer, Price = item.Price, RemainingNumber = item.RemainingNumber };
            var value = await _unitOfWork.ItemRepo.AddItem(new_item);
            if (value == true)
            {
 //               _logger.LogInformation("Added new item with id {0}", item.Id);
                await _unitOfWork.CompleteAsync();
            }
            return value;
        }

        public async Task<IEnumerable<IdentityUser>> GetCustomers()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
 //           _logger.LogInformation("Show list of customers.");

            return customers;
        }

        public async Task<RegistrationResponse> CreateAdmin(AdminRegisterRequest dto)
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
                var key = await _userManager.AddToRoleAsync(new_user, "Admin");
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
        public async Task<IEnumerable<IdentityUser>> GetAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            //           _logger.LogInformation("Show list of customers.");

            return admins;
        }

        public async Task<Item> Restock(int id,int number)
        {
            var item = await _unitOfWork.ItemRepo.Restock(id,number);
            return item;
        }
    }
}
