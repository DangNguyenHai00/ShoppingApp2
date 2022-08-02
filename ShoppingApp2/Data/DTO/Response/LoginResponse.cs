namespace ShoppingApp2.Data.DTO.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
