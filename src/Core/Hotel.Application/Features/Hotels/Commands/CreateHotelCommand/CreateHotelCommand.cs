using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Commands.CreateHotelCommand
{
    public class CreateHotelCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int CityId { get; set; }
    }
}
