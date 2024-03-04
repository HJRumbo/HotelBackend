using Hotel.Core.Domain.Common;

namespace Hotel.Core.Domain.Entities
{
    public class HotelClass : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
        public string Address { get; set; } = string.Empty;
        public int CityId { get; set; }
        public City? City { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
