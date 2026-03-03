using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.FaceRecognition.Commands;
using HRIS.Application.Features.Schedule.Commands;
using HRIS.Application.Features.Schedule.Queries;
using HRIS.Application.Features.ShiftTemplate.Commands;
using HRIS.Application.Features.ShiftTemplate.DTOs;
using HRIS.Application.Features.ShiftTemplate.Queries;
using HRIS.Application.Features.TimeEntries.Commands;
using HRIS.Application.Features.TimeEntries.Queries;
using HRIS.Domain.Enums;
using HRIS.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRIS_API.Controllers
{
    [ApiController]
    [Route("api/shift-templates")]
    public class ShiftTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShiftTemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllShiftTemplatesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetShiftTemplateByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateShiftTemplateDto dto)
        {
            var id = await _mediator.Send(new CreateShiftTemplateCommand(dto));
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateShiftTemplateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            await _mediator.Send(new UpdateShiftTemplateCommand(dto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteShiftTemplateCommand(id));
            return NoContent();
        }

    }
}
