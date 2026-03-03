using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.Commands
{
    public record UpdateShiftCommand(
        int ScheduleId,
        string NewShiftCode
    ) : IRequest;
}
