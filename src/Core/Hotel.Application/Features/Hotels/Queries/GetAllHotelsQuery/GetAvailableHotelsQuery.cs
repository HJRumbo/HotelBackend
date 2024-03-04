using Hotel.Core.Application.Dtos.Hotel;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Queries.GetAllHotelsQuery
{
    public class GetAvailableHotelsQuery : IRequest<Response<List<HotelDto>>>
    {
    }
}
