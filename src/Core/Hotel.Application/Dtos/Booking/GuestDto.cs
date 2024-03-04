namespace Hotel.Core.Application.Dtos.Booking
{
    public class GuestDto
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public int GenderId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
