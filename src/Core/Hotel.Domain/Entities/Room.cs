using Hotel.Core.Domain.Common;

namespace Hotel.Core.Domain.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
        public double BaseCost { get; set; }
        public double Taxes { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int HotelId { get; set; }
        public HotelClass? Hotel { get; set; }
    }
}
