using System.ComponentModel.DataAnnotations;

namespace ShoppingApp2.Data.DTO.Request
{
    public class RestockRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int number { get; set; }
    }
}
