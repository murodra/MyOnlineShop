using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Data.Entities;

namespace MyOnlineShop.Data
{
    public class MyOnlineShopDbContext:DbContext
    {
        public MyOnlineShopDbContext(DbContextOptions<MyOnlineShopDbContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(x => new {x.ProductId, x.OrderId} );
            modelBuilder.Entity<ProductStore>().HasKey(x => new {x.ProductId, x.OrderId} );
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductStore> ProductStores { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
