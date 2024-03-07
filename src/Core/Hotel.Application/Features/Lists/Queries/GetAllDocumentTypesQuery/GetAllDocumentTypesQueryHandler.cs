using AutoMapper;
using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllDocumentTypesQuery
{
    public class GetAllDocumentTypesQueryHandler : IRequestHandler<GetAllDocumentTypesQuery, Response<List<DocumentTypeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDocumentTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<DocumentTypeDto>>> Handle(GetAllDocumentTypesQuery request, CancellationToken cancellationToken)
        {
            var documentTypes = await _unitOfWork.DocumentTypes.GetAllAsync();

            var documentTypesDto = _mapper.Map<List<DocumentTypeDto>>(documentTypes);

            return new Response<List<DocumentTypeDto>>(documentTypesDto);
        }
    }
}
