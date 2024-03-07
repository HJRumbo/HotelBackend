using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllGendersQuery
{
    public class GetAllGendersQuery : IRequest<Response<List<GenderDto>>>
    {
    }
}
