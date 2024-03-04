using Hotel.Core.Application.Dtos.Booking;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Bookings.Queries.GetBookingsByUsernameQuery
{
    public class GetBookingsByUsernameQuery : IRequest<Response<List<BookingDto>>>
    {
        public string Username { get; set; } = string.Empty;
    }
}
