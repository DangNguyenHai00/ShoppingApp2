using Microsoft.AspNetCore.Identity;

namespace ShoppingApp2.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> All();
    }
}
