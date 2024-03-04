using Hotel.Core.Application.Features.Rooms.Commands.CreateRoomCommand;
using Hotel.Core.Application.Features.Rooms.Commands.UpdateRoomCommand;
using Hotel.Core.Application.Features.Rooms.Queries.GetRoomsByHotelIdQuery;
using Hotel.Core.Domain.Enums;
using Hotel.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    public class RoomController : BaseWebApiController
    {
        [HttpGet("{hotelId}", Name = "GetRoomsByHotelId")]
        public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
        {
            var request = new GetRoomsByHotelIdQuery()
            {
                HotelId = hotelId
            };

            var response = await Mediator!.Send(request);

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAsync(CreateRoomCommand request)
        {
            var response = await Mediator!.Send(request);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAsync(int id, UpdateRoomCommand request)
        {
            request.Id = id;

            var response = await Mediator!.Send(request);

            return Ok(response);
        }
    }
}
