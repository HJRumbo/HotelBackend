using Hotel.Core.Application.Features.Bookings.Commands.CreateBookingCommand;
using Hotel.Core.Application.Features.Bookings.Queries.GetAllBookingsQuery;
using Hotel.Core.Application.Features.Bookings.Queries.GetBookingsByUsernameQuery;
using Hotel.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    public class BookingController : BaseWebApiController
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBookingsByUsernameAsync()
        {
            return Ok(await Mediator!.Send(new GetAllBookingsQuery()));
        }

        [HttpGet("{username}", Name = "GetBookingsByUsername")]
        [Authorize(Roles = "Admin, Traveler")]
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
        [Authorize(Roles = "Traveler")]
        public async Task<IActionResult> PostAsync(CreateBookingCommand request)
        {
            var response = await Mediator!.Send(request);

            return Ok(response);
        }
    }
}
