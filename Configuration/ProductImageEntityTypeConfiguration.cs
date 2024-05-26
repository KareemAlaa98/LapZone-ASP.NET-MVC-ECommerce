using Microsoft.EntityFrameworkCore;
using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_GP.Configuration
{
    public class ProductImageEntityTypeConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GetDate()");

        }
    }
}
