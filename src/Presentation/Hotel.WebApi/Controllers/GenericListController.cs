using Hotel.Core.Application.Features.Lists.Queries.GetAllCitiesQuery;
using Hotel.Core.Application.Features.Lists.Queries.GetAllDocumentTypesQuery;
using Hotel.Core.Application.Features.Lists.Queries.GetAllGendersQuery;
using Hotel.Core.Application.Features.Lists.Queries.GetAllRoomTypesQuery;
using Hotel.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers
{
    public class GenericListController : BaseWebApiController
    {
        [HttpGet("GetAllCities")]
        public async Task<IActionResult> GetAllCitiesAsync()
        {
            return Ok(await Mediator!.Send(new GetAllCitiesQuery()));
        }

        [HttpGet("GetAllGenders")]
        public async Task<IActionResult> GetAllGendersAsync()
        {
            return Ok(await Mediator!.Send(new GetAllGendersQuery()));
        }

        [HttpGet("GetAllDocumentTypes")]
        public async Task<IActionResult> GetAllDocumentTypesAsync()
        {
            return Ok(await Mediator!.Send(new GetAllDocumentTypesQuery()));
        }

        [HttpGet("GetAllRoomTypes")]
        public async Task<IActionResult> GetAllRoomTypesAsync()
        {
            return Ok(await Mediator!.Send(new GetAllRoomTypesQuery()));
        }
    }
}
