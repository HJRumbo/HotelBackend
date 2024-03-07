using AutoMapper;
using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllGendersQuery
{
    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, Response<List<GenderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllGendersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<GenderDto>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            var genders = await _unitOfWork.Genders.GetAllAsync();

            var gendersDto = _mapper.Map<List<GenderDto>>(genders);

            return new Response<List<GenderDto>>(gendersDto);
        }
    }
}
