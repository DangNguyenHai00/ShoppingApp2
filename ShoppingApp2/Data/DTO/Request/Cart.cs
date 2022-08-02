using System.ComponentModel.DataAnnotations;
using ShoppingApp2.Data.Models;

namespace ShoppingApp2.Data.DTO.Request
{
    public class Cart
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public IEnumerable<PurchasingItem> Items { get; set; }
    }

    public class PurchasingItem
    {
        public int ItemId { get; set; }
        public int Number { get; set; }
    }
}
