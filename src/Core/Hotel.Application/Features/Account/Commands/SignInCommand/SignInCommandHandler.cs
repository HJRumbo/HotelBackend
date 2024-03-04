using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Interfaces.Infrastructure.Identity;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Account.Commands.SignInCommand
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, Response<SignInResponseDto>>
    {
        private readonly IAccountService _accountService;

        public SignInCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<SignInResponseDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.SignInAsync(new SignInRequestDto
            {
                UserName = request.UserName,
                Password = request.Password
            });
        }
    }
}
