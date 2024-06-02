using Microsoft.EntityFrameworkCore;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_GP.Configuration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder.HasCheckConstraint("CK_price_is_positive", "[Price] > 0");
            builder.HasCheckConstraint("CK_stock_is_positive", "[QuantityInStock] >= 0");

            builder.HasOne(p => p.Discount)
            .WithMany()
            .HasForeignKey(p => p.DiscountId)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
