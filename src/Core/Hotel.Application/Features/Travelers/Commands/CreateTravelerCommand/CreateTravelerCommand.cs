using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Travelers.Commands.CreateTravelerCommand
{
    public class CreateTravelerCommand : SignUpRequestDto, IRequest<Response<int>>
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public int GenderId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
    }
}
