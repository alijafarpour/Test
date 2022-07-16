using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Customer> builder)
        {
            builder.Property(t => t.Firstname)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Lastname)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.DateOfBirth)
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(320)
                .IsRequired();

            builder.Property(t => t.BankAccountNumber)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(t => new { t.Firstname, t.Lastname, t.DateOfBirth }).IsUnique(); // firstName, lastName and dateOfBirth should be unique together
            builder.HasIndex(t => t.Email).IsUnique();

            builder.HasKey(t => t.Id); // Set Primary key
        }
    }
}

