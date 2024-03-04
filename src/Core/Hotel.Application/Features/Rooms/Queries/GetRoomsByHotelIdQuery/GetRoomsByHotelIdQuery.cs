using Hotel.Core.Application.Dtos.Rooms;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Rooms.Queries.GetRoomsByHotelIdQuery
{
    public class GetRoomsByHotelIdQuery : IRequest<Response<List<RoomDto>>>
    {
        public int HotelId { get; set; }
    }
}
