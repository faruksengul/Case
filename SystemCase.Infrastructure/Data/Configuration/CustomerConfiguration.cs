using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemCase.Domain.Entities;

namespace SystemCase.Infrastructure.Data.Configuration;

public class CustomerConfiguration:IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(f => f.Name).HasMaxLength(150).IsRequired();
        builder.Property(f => f.LastName).HasMaxLength(150).IsRequired();
        builder.Property(f => f.Email).HasMaxLength(150).IsRequired();
    }
}