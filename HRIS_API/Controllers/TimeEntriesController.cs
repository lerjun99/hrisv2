using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.FaceRecognition.Commands;
using HRIS.Application.Features.TimeEntries.Commands;
using HRIS.Application.Features.TimeEntries.Queries;
using HRIS.Domain.Enums;
using HRIS.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRIS_API.Controllers
{
    [ApiController]
    [Route("api/time-entries")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeEntriesController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost("clock-in")]
        public async Task<IActionResult> ClockIn(int UserId,string userName)
            => Ok(await _mediator.Send(new ClockInCommand(UserId,userName)));

        [HttpPost("start-break")]
        public async Task<IActionResult> StartBreak(string userName, int UserId, BreakType type)
            => Ok(await _mediator.Send(new StartBreakCommand(userName, UserId, type)));

        [HttpPost("end-break")]
        public async Task<IActionResult> EndBreak(string userName, int UserId, BreakType type)
            => Ok(await _mediator.Send(new EndBreakCommand(userName, UserId, type)));

        [HttpPost("clock-out")]
        public async Task<IActionResult> ClockOut(string userName, int UserId)
            => Ok(await _mediator.Send(new ClockOutCommand(userName,UserId)));

        [HttpGet("getLogs")]
        public async Task<IActionResult> GetLogs(int UserId)
            => Ok(await _mediator.Send(new GetUserTimeEntriesQuery(UserId)));
        [HttpGet("active/{userName}")]
        public async Task<IActionResult> GetActiveEntry(string userName)
        {
            var result = await _mediator.Send(new GetActiveTimeEntryQuery(userName));

            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
