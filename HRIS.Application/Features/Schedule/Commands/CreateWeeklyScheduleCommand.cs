using HRIS.Application.Features.Schedule.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.Commands
{
    public record CreateWeeklyScheduleCommand(
        int UserId,
        List<CreateShiftDto> Shifts
    ) : IRequest;
}
