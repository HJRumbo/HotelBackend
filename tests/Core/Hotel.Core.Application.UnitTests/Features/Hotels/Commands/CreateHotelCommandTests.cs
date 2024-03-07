using AutoMapper;
using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Features.Hotels.Commands.CreateHotelCommand;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Domain.Entities;
using Moq;

namespace Hotel.Core.Application.UnitTests.Features.Hotels.Commands
{
    public class CreateHotelCommandTests
    {
        [Fact]
        public async Task Handle_Should_SaveHotel_When_IsValidCity()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var cityId = 42;
            var request = new CreateHotelCommand { CityId = cityId };
            var hotel = new HotelClass { Id = 1 };

            unitOfWorkMock.Setup(u => u.Cities.GetByIdAsync(cityId))
                .ReturnsAsync(new City());

            mapperMock.Setup(mapper => mapper.Map<HotelClass>(request))
                .Returns(hotel);

            var handler = new CreateHotelCommandHandler(unitOfWorkMock.Object, mapperMock.Object);
            
            unitOfWorkMock.Setup(u => u.Hotels.SaveAsync(hotel)).ReturnsAsync(hotel);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            Assert.Equal(hotel.Id, result.Data);
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_When_IsInvalidCity()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var cityId = 42;
            var request = new CreateHotelCommand { CityId = cityId };

            unitOfWorkMock.Setup(uow => uow.Cities.GetByIdAsync(cityId))
                .ReturnsAsync((City)null);

            var handler = new CreateHotelCommandHandler(unitOfWorkMock.Object, mapperMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
