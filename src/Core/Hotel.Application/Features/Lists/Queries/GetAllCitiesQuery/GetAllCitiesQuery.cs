using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllCitiesQuery
{
    public class GetAllCitiesQuery : IRequest<Response<List<CityDto>>>
    {
    }
}
