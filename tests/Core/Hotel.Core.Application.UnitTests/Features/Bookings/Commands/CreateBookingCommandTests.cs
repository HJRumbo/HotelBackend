using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Dtos.Booking;
using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Features.Bookings.Commands.CreateBookingCommand;
using Hotel.Core.Application.Interfaces.Infrastructure.Identity;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Interfaces.Infrastructure.Utils;
using Hotel.Core.Application.Models;
using Hotel.Core.Domain.Entities;
using Moq;

namespace Hotel.Core.Application.UnitTests.Features.Bookings.Commands
{
    public class CreateBookingCommandTests
    {
        private readonly CreateBookingCommand request;
        private readonly Booking booking;
        private readonly int roomId = 1;
        private readonly int travelerId = 1;
        private readonly string username = "testuser";

        public CreateBookingCommandTests()
        {

            request = new CreateBookingCommand
            {
                RoomId = roomId,
                Guests = new List<GuestDto>() {
                    new GuestDto() {
                        PhoneNumber = "",
                        Name = "",
                        LastName = "",
                        Birthday = It.IsAny<DateTime>(),
                        DocumentNumber = "",
                        DocumentTypeId = 1,
                        Email = "",
                        GenderId = 1
                    }
                },
                CheckIn = DateTime.Today.AddDays(1),
                CheckOut = DateTime.Today.AddDays(3),
                EmergencyContact = new EmergencyContactDto() { LastName = "", Name = "", PhoneNumber = "" },
                Username = username
            };

            booking = new Booking
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                RoomId = request.RoomId,
                Room = new Room { Name = "" },
                TravelerId = travelerId,
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
        }

        [Fact]
        public async Task Handle_Should_SaveBooking_When_BookingDataIsValid()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var accountMock = new Mock<IAccountService>();
            var emailMock = new Mock<IEmailService>();

            unitOfWorkMock.Setup(u => u.Rooms.GetByIdAsync(roomId))
                .ReturnsAsync(new Room() { Available = true, Capacity = 2 });

            unitOfWorkMock.Setup(u => u.Bookings.Where(It.IsAny<Func<Booking, bool>>()))
                .ReturnsAsync(new List<Booking>());

            unitOfWorkMock.Setup(u => u.Travelers.Where(It.IsAny<Func<Traveler, bool>>()))
                .ReturnsAsync(new List<Traveler>() { new Traveler { Id = travelerId, Name = "", LastName = "" } });

            accountMock.Setup(a => a.GetEmailByNameAsync(username)).ReturnsAsync(new SignInResponseDto() { Email = "" });

            unitOfWorkMock.Setup(u => u.Bookings.SaveAsync(It.IsAny<Booking>())).ReturnsAsync(booking);

            unitOfWorkMock.Setup(u => u.Hotels.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new HotelClass { Name = "" });

            emailMock.Setup(e => e.SendEmail(It.IsAny<Email>())).ReturnsAsync(true);

            var handler = new CreateBookingCommandHandler(unitOfWorkMock.Object, accountMock.Object, emailMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal(booking.Id, result.Data);
        }

        [Fact]
        public async Task Handle_Should_ShowMessage_When_EmailIsNotSent()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var accountMock = new Mock<IAccountService>();
            var emailMock = new Mock<IEmailService>();

            unitOfWorkMock.Setup(u => u.Rooms.GetByIdAsync(roomId))
                .ReturnsAsync(new Room() { Available = true, Capacity = 2 });

            unitOfWorkMock.Setup(u => u.Bookings.Where(It.IsAny<Func<Booking, bool>>()))
                .ReturnsAsync(new List<Booking>());

            unitOfWorkMock.Setup(u => u.Travelers.Where(It.IsAny<Func<Traveler, bool>>()))
                .ReturnsAsync(new List<Traveler>() { new Traveler { Id = travelerId, Name = "", LastName = "" } });

            accountMock.Setup(a => a.GetEmailByNameAsync(username)).ReturnsAsync(new SignInResponseDto() { Email = "" });

            unitOfWorkMock.Setup(u => u.Bookings.SaveAsync(It.IsAny<Booking>())).ReturnsAsync(booking);

            unitOfWorkMock.Setup(u => u.Hotels.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new HotelClass { Name = "" });

            emailMock.Setup(e => e.SendEmail(It.IsAny<Email>())).ReturnsAsync(false);

            var handler = new CreateBookingCommandHandler(unitOfWorkMock.Object, accountMock.Object, emailMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal("Reserva guardada correctamente, pero no se pudo enviar el correo electrónico. ", result.Message);
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_When_ThereAreBooking()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var accountMock = new Mock<IAccountService>();
            var emailMock = new Mock<IEmailService>();

            unitOfWorkMock.Setup(u => u.Rooms.GetByIdAsync(roomId))
                .ReturnsAsync(new Room() { Available = true, Capacity = 2 });

            unitOfWorkMock.Setup(u => u.Bookings.Where(It.IsAny<Func<Booking, bool>>()))
                .ReturnsAsync(new List<Booking>() { new Booking() });

            var handler = new CreateBookingCommandHandler(unitOfWorkMock.Object, accountMock.Object, emailMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
