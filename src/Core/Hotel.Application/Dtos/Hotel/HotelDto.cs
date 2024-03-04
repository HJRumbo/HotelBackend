namespace Hotel.Core.Application.Dtos.Hotel
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Available { get; set; }
        public string Address { get; set; } = string.Empty;
        public int CityId { get; set; }
    }
}
