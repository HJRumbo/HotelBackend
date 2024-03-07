using Hotel.Core.Application.Features.Travelers.Commands.CreateTravelerCommand;
using Hotel.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    public class TravelerController : BaseWebApiController
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> Post(CreateTravelerCommand request)
        {
            var response = await Mediator!.Send(request);

            return Ok(response);
        }
    }
}
