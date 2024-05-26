using E_Commerce_GP.Configuration;
using E_Commerce_GP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using E_Commerce_GP.ViewModels;
 

namespace E_Commerce_GP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProdcutImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public ApplicationDbContext() { }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new ApplicationUserEntityTypeConfiguration().Configure(builder.Entity<ApplicationUser>());
            new CategoryEntityTypeConfiguration().Configure(builder.Entity<Category>());
            new ProductEntityTypeConfiguration().Configure(builder.Entity<Product>());
            new ProductImageEntityTypeConfiguration().Configure(builder.Entity<ProductImage>());
            new DiscountEntityTypeConfiguration().Configure(builder.Entity<Discount>());
            new OrderEntityTypeConfiguration().Configure(builder.Entity<Order>());
            new PaymentEntityTypeConfiguration().Configure(builder.Entity<Payment>());
            new OrderItemsEntityTypeConfiguration().Configure(builder.Entity<OrderItem>());
            new ReviewEntityTypeConfiguration().Configure(builder.Entity<Review>());
            new WishlistEntityTypeConfiguration().Configure(builder.Entity<Wishlist>());
            new ContactUsEntityTypeConfiguration().Configure(builder.Entity<ContactUs>());
            new ShoppingCartEntityTypeConfigration().Configure(builder.Entity<ShoppingCart>());
            new CartItemEntityTypeConfigration().Configure(builder.Entity<CartItem>());
        }
   
    }
}
