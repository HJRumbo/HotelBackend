using Hotel.Core.Application.Dtos.Rooms;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Rooms.Queries.GetRoomsByHotelIdQuery
{
    public class GetRoomsByHotelIdQueryHandler : IRequestHandler<GetRoomsByHotelIdQuery, Response<List<RoomDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomsByHotelIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<RoomDto>>> Handle(GetRoomsByHotelIdQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _unitOfWork.Rooms.Where(x => x.Available && x.HotelId == request.HotelId);
            var roomsDto = rooms.Select(r => new RoomDto
            {
                Name = r.Name,
                Available = r.Available,
                BaseCost = r.BaseCost,
                Floor = r.Floor,
                RoomNumber = r.RoomNumber,
                RoomTypeId = r.RoomTypeId,
                Taxes = r.Taxes,
                Capacity = r.Capacity
            }).ToList();

            return new Response<List<RoomDto>>(roomsDto);
        }
    }
}
