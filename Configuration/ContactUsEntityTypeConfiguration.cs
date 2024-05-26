using E_Commerce_GP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_GP.Configuration
{
    public class ContactUsEntityTypeConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(e => e.CreatedAt)
    .HasDefaultValueSql("GetDate()");
        }
    }
}
