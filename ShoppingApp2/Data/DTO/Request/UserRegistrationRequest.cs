using System.ComponentModel.DataAnnotations;

namespace ShoppingApp2.Data.DTO.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
