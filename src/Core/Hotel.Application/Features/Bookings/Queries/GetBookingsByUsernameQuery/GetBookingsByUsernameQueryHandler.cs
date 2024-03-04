using Hotel.Core.Application.Dtos.Booking;
using Hotel.Core.Application.Interfaces.Infrastructure.Identity;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Bookings.Queries.GetBookingsByUsernameQuery
{
    public class GetBookingsByUsernameQueryHandler : IRequestHandler<GetBookingsByUsernameQuery, Response<List<BookingDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public GetBookingsByUsernameQueryHandler(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<Response<List<BookingDto>>> Handle(GetBookingsByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _accountService.GetEmailByNameAsync(request.Username) ?? throw new KeyNotFoundException($"El usuario con el nombre de usuario {request.Username} no se encuentra registrado. ");

            var travelers = await _unitOfWork.Travelers.Where(t => t.UserId == user.Id) ?? throw new KeyNotFoundException($"El usuario no se encuentra registrado. ");

            var traveler = travelers.FirstOrDefault();

            var bookings = await _unitOfWork.Bookings.GetAllWithIncludeAsync(x => x.Guests, x => x.EmergencyContact!);

            var travelerBookings = bookings.Where(t => t.TravelerId == traveler?.Id);

            var bookingsDto = travelerBookings.Select(r => new BookingDto
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
