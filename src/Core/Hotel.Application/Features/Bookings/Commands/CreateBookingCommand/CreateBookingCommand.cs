using Hotel.Core.Application.Dtos.Booking;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Bookings.Commands.CreateBookingCommand
{
    public class CreateBookingCommand : IRequest<Response<int>>
    {
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<GuestDto>? Guests { get; set; }
        public EmergencyContactDto? EmergencyContact { get; set; }
    }
}
