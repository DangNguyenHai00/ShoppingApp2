using System.ComponentModel.DataAnnotations;

namespace ShoppingApp2.Data.DTO.Request
{
    public class UserLoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
