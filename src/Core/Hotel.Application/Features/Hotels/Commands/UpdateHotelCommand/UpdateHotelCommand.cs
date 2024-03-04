using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Commands.UpdateHotelCommand
{
    public class UpdateHotelCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int CityId { get; set; }
        public bool Available { get; set; }
    }
}
