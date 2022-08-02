using ShoppingApp2.Repository.IRepository;

namespace ShoppingApp2.UnitOfWork
{
    public interface IUnitOfWork
    {
        IItemRepository ItemRepo { get; }
        IReceiptRepository ReceiptRepo { get; }
        Task CompleteAsync();
    }
}
