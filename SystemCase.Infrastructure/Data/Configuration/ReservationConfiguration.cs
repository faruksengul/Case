using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemCase.Domain.Entities;

namespace SystemCase.Infrastructure.Data.Configuration;

public class ReservationConfiguration:IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(f => f.CustomerId).IsRequired();
        builder.Property(f => f.ReservationDate).IsRequired();
    }
}