using Hotel.Core.Application.Dtos.Booking;
using Hotel.Core.Application.Interfaces.Infrastructure.Identity;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Bookings.Queries.GetAllBookingsQuery
{
    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, Response<List<BookingDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBookingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<BookingDto>>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _unitOfWork.Bookings.GetAllWithIncludeAsync(x => x.Guests, x => x.EmergencyContact!);

            var bookingsDto = bookings.Select(r => new BookingDto
            {
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                RoomId = r.RoomId,
                EmergencyContact = new EmergencyContactDto
                {
                    Name = r.EmergencyContact!.Name,
                    LastName = r.EmergencyContact.LastName,
                    PhoneNumber = r.EmergencyContact.PhoneNumber
                },
                Guests = r.Guests!.Select(g => new GuestDto
                {
                    Name = g.Name,
                    LastName = g.LastName,
                    PhoneNumber = g.PhoneNumber,
                    Birthday = g.Birthday,
                    GenderId = g.GenderId,
                    Email = g.Email,
                    DocumentTypeId = g.DocumentTypeId,
                    DocumentNumber = g.DocumentNumber
                }).ToList()
            }).ToList();

            return new Response<List<BookingDto>>(bookingsDto);
        }

    }
}
