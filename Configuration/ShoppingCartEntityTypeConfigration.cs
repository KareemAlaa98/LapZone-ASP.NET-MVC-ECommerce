using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_GP.Configuration
{
    public class ShoppingCartEntityTypeConfigration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GetDate()");


        }
    }
}
