using ShoppingApp2.Repository.IRepository;
using ShoppingApp2.Data;
using ShoppingApp2.Repository.Repository;

namespace ShoppingApp2.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IReceiptRepository ReceiptRepo { get; private set; }
        public IItemRepository ItemRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            ItemRepo = new ItemRepository(_context);
            ReceiptRepo = new ReceiptRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}