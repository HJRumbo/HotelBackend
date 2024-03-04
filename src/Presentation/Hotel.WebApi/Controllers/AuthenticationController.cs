using Hotel.Core.Application.Dtos.Authentication;
using Hotel.Core.Application.Features.Account.Commands.SignInCommand;
using Hotel.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    public class AuthenticationController : BaseWebApiController
    {
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(SignInRequestDto request)
        {
            var response = await Mediator!.Send(new SignInCommand
            {
                UserName = request.UserName,
                Password = request.Password
            });

            return Ok(response);
        }
    }
}
