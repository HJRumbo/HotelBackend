using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseWebApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator?>();
    }
}
