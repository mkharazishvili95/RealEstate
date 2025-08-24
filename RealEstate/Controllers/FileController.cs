using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Feature.File.Delete;
using RealEstate.Application.Feature.File.Upload;

namespace RealEstate.Controllers
{
    [Route("api/File")]
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

        [HttpDelete]
        public async Task<FileDeleteResponse> FileDelete([FromQuery] FileDeleteRequest request) => await _mediator.Send(request);
    }
}
