using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.FaceRecognition.Commands;
using HRIS.Application.Features.Schedule.Commands;
using HRIS.Application.Features.Schedule.Queries;
using HRIS.Application.Features.TimeEntries.Commands;
using HRIS.Application.Features.TimeEntries.Queries;
using HRIS.Domain.Enums;
using HRIS.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRIS_API.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/schedules/weekly?weekStart=2026-02-23
        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeekly([FromQuery] DateTime weekStart)
        {
            var result = await _mediator.Send(
                new GetWeeklyScheduleQuery(weekStart));

            return Ok(result);
        }

        // PUT: api/schedules/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateShift(UpdateShiftCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // POST: api/schedules/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateWeeklyScheduleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        // GET api/shifttemplates/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserShift(int userId)
        {
            var query = new GetUserShiftQuery { UserId = userId };
            var shift = await _mediator.Send(query);

            if (shift == null)
                return NoContent();

            return Ok(shift);
        }
    }
}
