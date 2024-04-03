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
        public DbSet<Shelf> Shelves { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.ShoppingList)
                .WithOne(sl => sl.User)
                .HasForeignKey<ShoppingList>(sl => sl.Id);

            modelBuilder.Entity<ShoppingList>()
                .HasMany(sl => sl.ProductsInList)
                .WithOne(pla => pla.ShoppingList);



            modelBuilder.Entity<ProductListAsc>()
                .HasOne(pla => pla.Product)
                .WithMany(p => p.ProductListAsc);

            
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
                .HasOne(i => i.Shelf)
                .WithMany(s => s.Items);

            modelBuilder.Entity<Store>()
                .HasMany(s => s.Shelves)
                .WithOne(i => i.Store);
            modelBuilder.Entity<Shelf>()
                .HasOne(s => s.Store)
                .WithMany(s => s.Shelves);
            modelBuilder.Entity<Shelf>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Shelf);
        }
    }
}
