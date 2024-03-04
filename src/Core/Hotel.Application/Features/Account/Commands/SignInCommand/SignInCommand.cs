using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Account.Commands.SignInCommand
{
    public class SignInCommand : IRequest<Response<SignInResponseDto>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
