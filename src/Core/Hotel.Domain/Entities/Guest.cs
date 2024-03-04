using Hotel.Core.Domain.Common;

namespace Hotel.Core.Domain.Entities
{
    public class Guest : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public int GenderId { get; set; }
        public Gender? Gender { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
