using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp2.Data.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ItemType { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int RemainingNumber { get; set; }

    }
}