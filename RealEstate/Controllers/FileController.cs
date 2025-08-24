using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.File.Upload;

namespace RealEstate.Controllers
{
    [Route("api/controller")]
    [ApiController]

    public class FileController : ControllerBase
    {
        readonly IMediator _mediator;
        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<FileUploadResponse> FileUpload(FileUploadRequest request) => await _mediator.Send(request);
    }
}
