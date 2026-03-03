using HRIS.Application.Features.Schedule.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.Queries
{
    public record GetWeeklyScheduleQuery(DateTime WeekStartDate)
        : IRequest<List<EmployeeScheduleDto>>;
}
