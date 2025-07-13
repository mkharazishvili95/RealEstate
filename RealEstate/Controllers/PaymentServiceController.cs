using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.Profile.Transfer;
using RealEstate.Application.Services;

namespace RealEstate.Controllers
{
    [ApiController]
    [Route("api/PaidService")]
    public class PaymentServiceController : ControllerBase
    {
        private readonly IPaymentService _paidService;
        public PaymentServiceController(IPaymentService paidService)
        {
            _paidService = paidService;
        }
        [HttpPost("transfer")]
        public async Task<TransferBalanceResponse> TransferBalance([FromBody] TransferBalanceRequest request) => await _paidService.TransferBalanceAsync(request);
    }
}
