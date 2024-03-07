using AutoMapper;
using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllRoomTypesQuery
{
    public class GetAllRoomTypesQueryHandler : IRequestHandler<GetAllRoomTypesQuery, Response<List<RoomTypeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRoomTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<RoomTypeDto>>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var roomTypes = await _unitOfWork.RoomTypes.GetAllAsync();

            var roomTypesDto = _mapper.Map<List<RoomTypeDto>>(roomTypes);

            return new Response<List<RoomTypeDto>>(roomTypesDto);
        }
    }
}
