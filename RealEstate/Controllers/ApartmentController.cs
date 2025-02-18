using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("api/Apartment")]
    public class ApartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
