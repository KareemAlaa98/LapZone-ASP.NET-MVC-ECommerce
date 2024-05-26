using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_GP.Configuration
{
    public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GetDate()");

            builder.HasCheckConstraint("CK_discount_is_positive", "[DiscountPercent] > 0");

        }
    }
}
