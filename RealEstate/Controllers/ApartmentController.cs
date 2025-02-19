using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Infrastructure.Queries.Apartment;
using RealEstate.Infrastructure.Queries.Models.Apartment;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("api/Apartment")]
    public class ApartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApartmentQueries _apartmentQueries;
        public ApartmentController(IMediator mediator, IApartmentQueries apartmentQueries)
        {
            _mediator = mediator;
            _apartmentQueries = apartmentQueries;
        }

        [HttpGet("apartment-details")]
        public async Task<GetApartmentDetailsModel> GetApartmentDetails(int apartmentId) => await _apartmentQueries.GetApartment(apartmentId);

        [HttpGet("new-apartments")]
        public async Task<GetNewApartmentsModel> GetNewApartments() => await _apartmentQueries.GetNewApartments();
    }
}
