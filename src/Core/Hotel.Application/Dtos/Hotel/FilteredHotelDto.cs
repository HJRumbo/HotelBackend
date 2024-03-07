using Hotel.Core.Application.Dtos.Rooms;

namespace Hotel.Core.Application.Dtos.Hotel
{
    public class FilteredHotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Available { get; set; }
        public string Address { get; set; } = string.Empty;
        public int CityId { get; set; }
        public List<RoomDto>? Rooms { get; set; }
    }
}
