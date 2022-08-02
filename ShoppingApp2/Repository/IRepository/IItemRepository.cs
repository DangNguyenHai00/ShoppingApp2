using ShoppingApp2.Data.Models;

namespace ShoppingApp2.Repository.IRepository
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> All();
        Task<bool> AddItem(Item item);
        Task<Item> GetById(int id);
        Task<bool> TakeOutItem(int id, int number);
    }
}
