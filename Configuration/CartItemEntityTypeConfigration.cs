using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Configuration
{
    public class CartItemEntityTypeConfigration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GetDate()");
            builder.HasCheckConstraint("CK_Quantity_is_positive", "[Quantity] > 0");
        }
    
    }
}
