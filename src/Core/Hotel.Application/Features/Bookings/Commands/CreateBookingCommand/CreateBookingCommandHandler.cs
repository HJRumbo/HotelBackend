using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Interfaces.Infrastructure.Identity;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Interfaces.Infrastructure.Utils;
using Hotel.Core.Application.Models;
using Hotel.Core.Domain.Entities;
using MediatR;

namespace Hotel.Core.Application.Features.Bookings.Commands.CreateBookingCommand
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        public CreateBookingCommandHandler(IUnitOfWork unitOfWork, IAccountService accountService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _emailService = emailService;
        }

        public async Task<Response<int>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            await ValidateBookingData(request);

            var user = await ValidateUser(request.Username);

            var traveler = await ValidateTraveler(user.Id);

            var booking = new Booking
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                RoomId = request.RoomId,
                TravelerId = traveler!.Id,
                EmergencyContact = new EmergencyContact
                {
                    Name = request.EmergencyContact!.Name,
                    LastName = request.EmergencyContact.LastName,
                    PhoneNumber = request.EmergencyContact.PhoneNumber
                },
                Guests = request.Guests!.Select(g => new Guest
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
            };

            var registredBooking = await _unitOfWork.Bookings.SaveAsync(booking);

            await _unitOfWork.SaveChangesAsync();

            var sentEmail = await SendEmail(user.Email, traveler.Name + " " + traveler.LastName, registredBooking);

            return new Response<int>(registredBooking.Id);
        }

        private async Task ValidateBookingData(CreateBookingCommand request)
        {
            List<string> errors = new();

            var room = await _unitOfWork.Rooms.GetByIdAsync(request.RoomId);

            if (room == null)
            {
                errors.Add($"La habitación con id {request.RoomId} no existe, consulte las habitaciones disponibles. ");
            }
            else if (!room.Available)
            {
                errors.Add($"La habitación con id {request.RoomId} ya se encuentra reservada. ");
            }
            else if (room.Capacity < request.Guests!.Count + 1)
            {
                errors.Add($"La habitación cuenta con capacidad para {room.Capacity} personas. ");
            }

            var bookedRoom = await _unitOfWork.Bookings.Where(b => b.RoomId == request.RoomId && (request.CheckIn >= b.CheckIn && request.CheckIn <= b.CheckOut));

            if (bookedRoom.Any())
            {
                errors.Add("La habitación ya se encuentra reservada para la fecha de ingreso digitada. ");
            }


            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
        }

        private async Task<SignInResponseDto> ValidateUser(string username)
        {
            return await _accountService.GetEmailByNameAsync(username) ?? throw new KeyNotFoundException($"El usuario con el nombre de usuario {username} no se encuentra registrado. ");
        }

        private async Task<Traveler?> ValidateTraveler(string userId)
        {
            var travelers = await _unitOfWork.Travelers.Where(t => t.UserId == userId) ?? throw new KeyNotFoundException($"El usuario no se encuentra registrado. ");

            return travelers.FirstOrDefault();
        }

        private async Task<bool> SendEmail(string email, string fullName, Booking registredBooking)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAsync(registredBooking.Room!.HotelId);

            string body = @$"<div>
                                <h1>!Hola, {fullName}¡</h1>

                                <P>Te confirmamos tu reverva en el Hotel <strong>{hotel!.Name}</strong></P>

                                <p>Aquí te dejamos los detalles de la reserva: </p>

                                <h3>Habitación: <p>{registredBooking.Room.Name}</p></h3> 
                                <p>Tiempo de estadía: {registredBooking.CheckIn} - {registredBooking.CheckOut}</p>  

                                <p>Gracias por elegirnos. </p>
                            </div>";

            var emailToSend = new Email
            {
                To = email,
                Subject = "Confirmación de reserva",
                Body = body
            };

            return await _emailService.SendEmail(emailToSend);
        }
    }
}
