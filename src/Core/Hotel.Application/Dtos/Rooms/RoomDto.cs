namespace Hotel.Core.Application.Dtos.Rooms
{
    public class RoomDto
    {
        public string Name { get; set; } = string.Empty;
        public bool Available { get; set; }
        public double BaseCost { get; set; }
        public double Taxes { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public int RoomNumber { get; set; }
    }
}
