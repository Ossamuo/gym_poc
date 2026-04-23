using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Contexts.ActivitiesContext.Mapping;

public class BookingMap : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder
            .HasKey(x => x.Id)
            .HasName("PK_Bookings");

        builder.Property(x => x.MemberId)
            .HasColumnName("MemberId")
            .IsRequired();

        builder.Property(x => x.ActivityId)
            .HasColumnName("ActivityId")
            .IsRequired();

        builder.Property(x => x.PartnerId)
            .HasColumnName("PartnerId")
            .IsRequired();

        builder.Property(x => x.CheckInAt)
            .HasColumnName("CheckInAt")
            .HasColumnType("datetime2");

        builder.Property(x => x.CancelAt)
            .HasColumnName("CancelAt")
            .HasColumnType("datetime2");

        builder.Property(x => x.Status)
            .HasColumnName("Status")
            .HasConversion<int>()
            .IsRequired();
    }
}