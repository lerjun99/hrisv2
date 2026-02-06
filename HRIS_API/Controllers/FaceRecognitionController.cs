using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.FaceRecognition.Commands;
using HRIS.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRIS_API.Controllers
{
    //[Authorize("ApiKey")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class FaceRecognitionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHrisDbContext _hrisdbcontext;
        public FaceRecognitionController(IMediator mediator, IHrisDbContext hrisdbcontext)
        {
            _mediator = mediator;
            _hrisdbcontext = hrisdbcontext;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterFaceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("identify")]
        public async Task<IActionResult> Identify(IdentifyFaceCommand command)
        {
            var userId = await _mediator.Send(command);
            return userId == null ? Unauthorized("Face not recognized or invalid image") : Ok(userId);
        }
    }
}
