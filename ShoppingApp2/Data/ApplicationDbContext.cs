using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingApp2.Data.Models;

namespace ShoppingApp2.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Receipt> Receipts { get; set; } = null!;
        //        public DbSet<User> Users { get; set; } = null!;
    }
}
