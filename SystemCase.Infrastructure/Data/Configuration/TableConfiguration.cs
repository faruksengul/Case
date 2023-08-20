using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemCase.Domain.Entities;

namespace SystemCase.Infrastructure.Data.Configuration;

public class TableConfiguration:IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.Property(f => f.Number).IsRequired();
        builder.Property(f => f.Capacity).IsRequired();
        builder.Property(f => f.TableType).IsRequired();
    }
}