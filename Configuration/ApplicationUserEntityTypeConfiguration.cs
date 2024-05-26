using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_GP.Configuration
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder.HasIndex(e => e.PhoneNumber).IsUnique();

            builder.HasCheckConstraint("CK_FloorNumber_IsPositive", "[Floor_Number] >= 0");
            builder.HasCheckConstraint("CK_BuildingNumber_IsPositive", "[Building_Number] >= 0");
        }
    }
}
