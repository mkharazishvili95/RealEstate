using MediatR;
using RealEstate.Application.Job.Apartment.Expire;

namespace RealEstate_Jobs.Services
{
    public interface IApartmentJobService
    {
        Task ExpireApartments();
    }

    public class ApartmentJobService : IApartmentJobService
    {
        private readonly IMediator _mediator;

        public ApartmentJobService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ExpireApartments()
        {
            var command = new ExpireApartmentsCommand();
            await _mediator.Send(command);
        }
    }
}
