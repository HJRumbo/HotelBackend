using Hotel.Core.Application.Dtos.Hotel;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Queries.GetAllHotelsQuery
{
    public class GetAvailableHotelsQueryHandler : IRequestHandler<GetAvailableHotelsQuery, Response<List<HotelDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAvailableHotelsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<HotelDto>>> Handle(GetAvailableHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _unitOfWork.Hotels.Where(x => x.Available);
            var hotelsDto = hotels.Select(x => new HotelDto
            {
                Id = x.Id,
                Address = x.Address,
                Available = x.Available,
                CityId = x.CityId,
                Name = x.Name
            }).ToList();

            return new Response<List<HotelDto>>(hotelsDto);
        }
    }
}
