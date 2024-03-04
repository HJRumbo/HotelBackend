using Hotel.Core.Application.Features.Bookings.Commands.CreateBookingCommand;
using Hotel.Core.Application.Features.Bookings.Queries.GetBookingsByUsernameQuery;
using Hotel.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    [Authorize(Roles = "Admin, Traveler")]
    public class BookingController : BaseWebApiController
    {
        [HttpGet("{username}", Name = "GetBookingsByUsername")]
        public async Task<IActionResult> GetBookingsByUsernameAsync(string username)
        {
            var request = new GetBookingsByUsernameQuery()
            {
                Username = username
            };

            var response = await Mediator!.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateBookingCommand request)
        {
            var response = await Mediator!.Send(request);

            return Ok(response);
        }
    }
}
