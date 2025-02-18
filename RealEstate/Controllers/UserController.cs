using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
