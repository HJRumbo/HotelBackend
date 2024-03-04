using Hotel.Core.Domain.Common;

namespace Hotel.Core.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int TravelerId { get; set; }
        public Traveler? Traveler { get; set; }
        public List<Guest> Guests { get; set; } = new();
        public int EmergencyContactId { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }
    }
}
