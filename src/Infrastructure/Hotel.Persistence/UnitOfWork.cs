using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Domain.Entities;
using Hotel.Infrastructure.Persistence.Context;
using Hotel.Infrastructure.Persistence.Repositories;

namespace Hotel.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IGenericRepositoryAsync<HotelClass> Hotels { get; }
        public IGenericRepositoryAsync<Traveler> Travelers { get; }
        public IGenericRepositoryAsync<DocumentType> DocumentTypes { get; }
        public IGenericRepositoryAsync<Gender> Genders { get; }
        public IGenericRepositoryAsync<City> Cities { get; }
        public IGenericRepositoryAsync<Room> Rooms { get; }
        public IGenericRepositoryAsync<RoomType> RoomTypes { get; }
        public IGenericRepositoryAsync<Booking> Bookings { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Hotels = new GenericRepositoryAsync<HotelClass>(_context);
            Travelers = new GenericRepositoryAsync<Traveler>(_context);
            DocumentTypes = new GenericRepositoryAsync<DocumentType>(_context);
            Genders = new GenericRepositoryAsync<Gender>(_context);
            Cities = new GenericRepositoryAsync<City>(_context);
            Rooms = new GenericRepositoryAsync<Room>(_context);
            RoomTypes = new GenericRepositoryAsync<RoomType>(_context);
            Bookings = new GenericRepositoryAsync<Booking>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
