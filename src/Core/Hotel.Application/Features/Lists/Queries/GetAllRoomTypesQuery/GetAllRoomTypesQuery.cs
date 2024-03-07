using Hotel.Core.Application.Dtos.Lists;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Lists.Queries.GetAllRoomTypesQuery
{
    public class GetAllRoomTypesQuery : IRequest<Response<List<RoomTypeDto>>>
    { 
    }
}
