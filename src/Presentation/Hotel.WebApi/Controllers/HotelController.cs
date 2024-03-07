using Hotel.Core.Application.Features.Hotels.Commands.CreateHotelCommand;
using Hotel.Core.Application.Features.Hotels.Commands.UpdateHotelCommand;
using Hotel.Core.Application.Features.Hotels.Queries.GetAllHotelsQuery;
using Hotel.Core.Application.Features.Hotels.Queries.GetFilteredHotelsQuery;
using Hotel.Core.Domain.Enums;
using Hotel.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    public class HotelController : BaseWebApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await Mediator!.Send(new GetAvailableHotelsQuery()));
        }

        [HttpGet("GetFilteredHotels")]
        public async Task<IActionResult> GetAsync([FromQuery] GetFilteredHotelsQuery request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAsync(CreateHotelCommand request)
        {
            var response = await Mediator!.Send(request);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAsync(int id, UpdateHotelCommand request)
        {
            request.Id = id;

            var response = await Mediator!.Send(request);

            return Ok(response);
        }
    }
}
