namespace ShoppingApp2.Data.DTO.Response
{
    public class PurchaseResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public double Total { get; set; }
    }
}
