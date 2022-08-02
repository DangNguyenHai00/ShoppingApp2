using ShoppingApp2.Data.Models;

namespace ShoppingApp2.Repository.IRepository
{
    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> All();
        bool NewReceipt(Receipt receipt);
    }
}
