using AutoMapper;
using Hotel.Core.Application.Dtos.Hotel;
using Hotel.Core.Application.Dtos.Rooms;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Queries.GetFilteredHotelsQuery
{
    public class GetFilteredHotelsQueryHandler : IRequestHandler<GetFilteredHotelsQuery, Response<List<FilteredHotelDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFilteredHotelsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<FilteredHotelDto>>> Handle(GetFilteredHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _unitOfWork.Hotels.GetFilteredHotels(request.CityId, request.CheckIn, request.CheckOut, request.Capacity);

            var hotelsDto = hotels.Select(h => new FilteredHotelDto
            {
                Address = h.Address,
                Available = h.Available,
                CityId = h.CityId,
                Id = h.Id,
                Name = h.Name,
                Rooms = h.Rooms.Select(r => new RoomDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Available = r.Available,
                    BaseCost = r.BaseCost,
                    Capacity = r.Capacity,
                    Floor = r.Floor,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId, 
                    Taxes = r.Taxes
                }).ToList()
            }).ToList();

            return new Response<List<FilteredHotelDto>>(hotelsDto);
        }
    }
}
