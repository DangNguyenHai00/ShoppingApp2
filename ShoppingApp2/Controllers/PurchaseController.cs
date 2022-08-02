using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ShoppingApp2.Service;
using ShoppingApp2.Data.DTO.Request;

namespace ShoppingApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Customer")]
    public class PurchaseController:ControllerBase
    {
        private readonly ShopManagement _shopManagament;
        public PurchaseController(ShopManagement shopManagement)
        {
            _shopManagament = shopManagement;
        }

        [HttpPost]
        [Route("Purchase")]
        public async Task<IActionResult> PurchaseCart([FromBody] Cart cart)
        {
            var response = await _shopManagament.PurchaseItems(cart);
            return Ok(response);
        }

    }
}
