using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp2.Data.Models
{
    public class Receipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptId { get; set; }
        [Required]
        public double TotalPurchased { get; set; }
        [Required]
        public string UserName { get; set; }
//        public ICollection<Item> Items { get; set; }
    }
}
