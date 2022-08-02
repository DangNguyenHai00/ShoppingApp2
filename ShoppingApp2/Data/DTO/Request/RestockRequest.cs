using System.ComponentModel.DataAnnotations;

namespace ShoppingApp2.Data.DTO.Request
{
    public class RestockRequest
    {
        [Required]
        public int Id;
        [Required]
        public int number;
    }
}
