using Microsoft.EntityFrameworkCore;
using ScanAndGoApi.Models;
namespace ScanAndGoApi.Context
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ProductListAsc> ProductListAsc { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.ShoppingLists)
                .WithOne(sl => sl.User);

            modelBuilder.Entity<ShoppingList>()
                .HasMany(sl => sl.ProductsInList)
                .WithOne(pla => pla.ShoppingList);

            modelBuilder.Entity<ShoppingList>()
                .HasOne(sl => sl.User)
                .WithMany(u => u.ShoppingLists);

            modelBuilder.Entity<ProductListAsc>()
                .HasOne(pla => pla.Product)
                .WithMany(p => p.ProductListAsc);

            modelBuilder.Entity<ProductListAsc>()
                .HasOne(pla => pla.ShoppingList)
                .WithMany(sl => sl.ProductsInList);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Items)
                .WithOne(i => i.Product);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductListAsc)
                .WithOne(pla => pla.Product);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Items);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Store)
                .WithMany(s => s.Items);

            modelBuilder.Entity<Store>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Store);

        }
    }
}
