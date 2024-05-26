using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_GP.Configuration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GetDate()");

            builder.HasCheckConstraint("CK_total_is_positive", "[Total] > 0");

            //builder.HasOne

        }
    }
}
