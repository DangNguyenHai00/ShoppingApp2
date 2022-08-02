using ShoppingApp2.Repository.IRepository;
using ShoppingApp2.Data;
using ShoppingApp2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingApp2.Repository.Repository
{
    public class ReceiptRepository:IReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        public ReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Receipt>> All()
        {
            return await _context.Receipts.ToListAsync();
        }

        public async Task<Receipt> GetById(int id)
        {
            var exist = await _context.Receipts.Where(x => id == x.ReceiptId).FirstOrDefaultAsync();
            return exist;
        }
        public async Task<bool> Delete(int id)
        {
            var exist = await _context.Receipts.Where(x => id == x.ReceiptId).FirstOrDefaultAsync();

            if (exist != null)
            {
                _context.Receipts.Remove(exist);
                return true;
            }
            return false;
        }

        public bool NewReceipt(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            return true;
        }
    }
}
