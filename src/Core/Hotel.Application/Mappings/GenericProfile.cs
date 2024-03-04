using AutoMapper;
using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Dtos.Hotel;
using Hotel.Core.Application.Features.Bookings.Commands.CreateBookingCommand;
using Hotel.Core.Application.Features.Hotels.Commands.CreateHotelCommand;
using Hotel.Core.Application.Features.Rooms.Commands.CreateRoomCommand;
using Hotel.Core.Application.Features.Travelers.Commands.CreateTravelerCommand;
using Hotel.Core.Domain.Entities;

namespace Hotel.Core.Application.Mappings
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            #region Entities to Dtos
            CreateMap<HotelClass, HotelDto>();
            #endregion

            #region Commands to Entities
            CreateMap<CreateHotelCommand, HotelClass>();
            CreateMap<CreateTravelerCommand, Traveler>();
            CreateMap<CreateRoomCommand, Room>();
            CreateMap<CreateBookingCommand, Booking>();
            #endregion

            #region CreateUserCommand to RegisterUserRequestDto
            CreateMap<CreateTravelerCommand, SignUpRequestDto>();
            #endregion
        }
    }
}
