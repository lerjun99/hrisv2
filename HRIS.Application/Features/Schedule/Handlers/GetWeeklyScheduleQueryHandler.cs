using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.Schedule.DTOs;
using HRIS.Application.Features.Schedule.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.Handlers
{
    public class GetWeeklyScheduleQueryHandler
     : IRequestHandler<GetWeeklyScheduleQuery, List<EmployeeScheduleDto>>
    {
        private readonly IHrisDbContext _context;

        public GetWeeklyScheduleQueryHandler(IHrisDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeScheduleDto>> Handle( GetWeeklyScheduleQuery request,CancellationToken cancellationToken)
        {
            var shiftTemplates = await _context.ShiftTemplates
              .ToDictionaryAsync(s => s.Code, cancellationToken);

            var employees = await _context.Employees
                .Include(e => e.Schedules)
                .ToListAsync(cancellationToken);

            var result = employees.Select(e => new EmployeeScheduleDto
            {
                UserId = e.Id,
                Username = e.Username,
                WeeklyShifts = e.Schedules
                    .OrderBy(s => s.Day)
                    .Select(s =>
                    {
                        shiftTemplates.TryGetValue(s.ShiftCode, out var template);

                        return new ShiftDto
                        {
                            ScheduleId = s.Id,
                            Day = s.Day,
                            ShiftCode = s.ShiftCode,
                            TimeIn = template != null
                                ? template.TimeIn.ToString(@"hh\:mm")
                                : "OFF",
                            TimeOut = template != null
                                ? template.TimeOut.ToString(@"hh\:mm")
                                : "OFF",
                            BreakMinutes = template?.BreakMinutes ?? 0,
                            IsWeekend = s.Day == DayOfWeek.Saturday ||
                                        s.Day == DayOfWeek.Sunday
                        };
                    }).ToList()
            }).ToList();

            return result;
        }
    }
}
