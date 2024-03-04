namespace Hotel.Core.Application.Dtos.Booking
{
    public class BookingDto
    {
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<GuestDto>? Guests { get; set; }
        public EmergencyContactDto? EmergencyContact { get; set; }
    }
}
