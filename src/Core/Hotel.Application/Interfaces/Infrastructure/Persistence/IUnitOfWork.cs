
using Hotel.Core.Domain.Entities;

namespace Hotel.Core.Application.Interfaces.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IGenericRepositoryAsync<HotelClass> Hotels { get; }
        IGenericRepositoryAsync<Traveler> Travelers { get; }
        IGenericRepositoryAsync<DocumentType> DocumentTypes { get; }
        IGenericRepositoryAsync<Gender> Genders { get; }
        IGenericRepositoryAsync<City> Cities { get; }
        IGenericRepositoryAsync<Room> Rooms { get; }
        IGenericRepositoryAsync<RoomType> RoomTypes { get; }
        IGenericRepositoryAsync<Booking> Bookings { get; }
        Task SaveChangesAsync();
    }
}
