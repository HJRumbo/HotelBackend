using Hotel.Core.Domain.Entities;

namespace Hotel.Core.Application.Interfaces.Infrastructure.Persistence.Repositories
{
    public interface IHotelRepository : IGenericRepositoryAsync<HotelClass>
    {
        Task<List<HotelClass>> GetFilteredHotels(int? cityId, DateTime? checkIn, DateTime? checkOut, int? capacity);
    }
}
