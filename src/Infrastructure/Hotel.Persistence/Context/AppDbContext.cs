using Hotel.Core.Domain.Common;
using Hotel.Core.Domain.Entities;
using Hotel.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BookingConfiguration().Configure(modelBuilder.Entity<Booking>());
            new GuestConfiguration().Configure(modelBuilder.Entity<Guest>());
            new HotelConfiguration().Configure(modelBuilder.Entity<HotelClass>());
            new RoomConfiguration().Configure(modelBuilder.Entity<Room>());
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<HotelClass> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Traveler> Travelers { get; set; }
    }
}
