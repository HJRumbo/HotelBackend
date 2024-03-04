using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Rooms.Commands.UpdateRoomCommand
{
    public class UpdateRoomCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double BaseCost { get; set; }
        public double Taxes { get; set; }
        public int RoomTypeId { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int HotelId { get; set; }
        public bool Available { get; set; }
    }
}
