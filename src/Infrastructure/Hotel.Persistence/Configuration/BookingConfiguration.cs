using Hotel.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Infrastructure.Persistence.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Traveler)
                .WithMany()
                .HasForeignKey(x => x.TravelerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.EmergencyContact)
                .WithMany()
                .HasForeignKey(x => x.EmergencyContactId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
