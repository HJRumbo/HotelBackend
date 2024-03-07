using AutoMapper;
using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllCitiesQuery
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, Response<List<CityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _unitOfWork.Cities.GetAllAsync();
            
            var citiesDto = _mapper.Map<List<CityDto>>(cities);

            return new Response<List<CityDto>>(citiesDto);
        }
    }
}
