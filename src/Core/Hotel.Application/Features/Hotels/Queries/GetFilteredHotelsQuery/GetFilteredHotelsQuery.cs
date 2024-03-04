using Hotel.Core.Application.Dtos.Hotel;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Queries.GetFilteredHotelsQuery
{
    public class GetFilteredHotelsQuery : IRequest<Response<List<HotelDto>>>
    {
        public int Capacity { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string? CityName { get; set; }
    }
}
