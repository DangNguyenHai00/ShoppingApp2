using ShoppingApp2.Data.DTO.Request;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp2.Data.DTO.Request
{
    public class PurchaseRequest
    {
        public Cart cart { get; set; }
    }
}
