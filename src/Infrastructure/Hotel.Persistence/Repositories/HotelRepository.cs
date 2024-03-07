using Hotel.Core.Application.Interfaces.Infrastructure.Persistence.Repositories;
using Hotel.Core.Domain.Entities;
using Hotel.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Persistence.Repositories
{
    public class HotelRepository : GenericRepositoryAsync<HotelClass>, IHotelRepository
    {
        public HotelRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<HotelClass>> GetFilteredHotels(int? cityId, DateTime? checkIn, DateTime? checkOut, int? capacity)
        {
            var hotels = cityId.HasValue ? await _context.Hotels.Where(h => h.CityId == cityId).Include(h => h.Rooms).ToListAsync()
                : await _context.Hotels.Include(h => h.Rooms).ToListAsync();

            int? numberOfPeople = 0;

            if (capacity.HasValue)
            {
                numberOfPeople = capacity;

                hotels = hotels.Where(h => h.Rooms!.Any()).ToList();
            }

            var filteredHotels = new List<HotelClass>();

            hotels.ForEach(h => {
                h.Rooms = h.Rooms!.Where(r => r.Available && r.Capacity >= numberOfPeople).ToList();

                if (checkIn.HasValue && checkOut.HasValue)
                {
                    var rooms = new List<Room>();

                    h.Rooms!.ForEach(r => {
                        if (!_context.Bookings.Where(b => b.RoomId == r.Id && b.CheckIn <= checkOut && b.CheckOut >= checkIn).Any())
                        {
                            rooms.Add(r);
                        }
                    });

                    h.Rooms = rooms;
                }

                if (h.Rooms.Any())
                {
                    filteredHotels.Add(h);
                }
            });

            return filteredHotels;

        }
    }
}
