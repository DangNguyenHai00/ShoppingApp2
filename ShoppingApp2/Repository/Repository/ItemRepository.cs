using Microsoft.EntityFrameworkCore;
using ShoppingApp2.Data.Models;
using ShoppingApp2.Data;
using ShoppingApp2.Repository.IRepository;

namespace ShoppingApp2.Repository.Repository
{
    public class ItemRepository:IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> All()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetById(int id)
        {
            var exist = await _context.Items.Where(x => id == x.Id).FirstOrDefaultAsync();
            return exist;
        }

        public async Task<bool> AddItem(Item item)
        {
            var exist = await _context.Items.Where(x => item.Id == x.Id).FirstOrDefaultAsync();

            if (exist == null)
            {
                _context.Items.Add(item);
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(int id)
        {
            var exist = await _context.Items.Where(x => id == x.Id).FirstOrDefaultAsync();

            if (exist != null)
            {
                _context.Items.Remove(exist);
                return true;
            }
            return false;
        }

        public async Task<bool> RestockItem(int id, int number)
        {
            var exist = await _context.Items.Where(x => id == x.Id).FirstOrDefaultAsync();

            if (exist != null)
            {
                exist.RemainingNumber += number;
                return true;
            }
            return false;
        }

        public async Task<Item> TakeOutItem(int id, int number)
        {
            var exist = await _context.Items.Where(x => id == x.Id).FirstOrDefaultAsync();

            if (exist != null && exist.RemainingNumber > 0)
            {
                exist.RemainingNumber -= number;
                return exist;
            }
            return null;
        }
    }
}
