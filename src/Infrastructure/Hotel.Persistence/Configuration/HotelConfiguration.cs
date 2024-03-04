using Hotel.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Infrastructure.Persistence.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<HotelClass>
    {
        public void Configure(EntityTypeBuilder<HotelClass> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
