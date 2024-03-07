using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllDocumentTypesQuery
{
    public class GetAllDocumentTypesQuery : IRequest<Response<List<DocumentTypeDto>>>
    {
    }
}
