using Hotel.Core.Application.Dtos.Booking;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Bookings.Queries.GetAllBookingsQuery
{
    public class GetAllBookingsQuery : IRequest<Response<List<BookingDto>>>
    {
    }
}
