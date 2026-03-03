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
    public class GetUserShiftQueryHandler : IRequestHandler<GetUserShiftQuery, UserShiftDto?>
    {
        private readonly IHrisDbContext _context;

        public GetUserShiftQueryHandler(IHrisDbContext context)
        {
            _context = context;
        }

        public async Task<UserShiftDto?> Handle(GetUserShiftQuery request, CancellationToken cancellationToken)
        {
            // Assuming you have a ShiftTemplate table linked to Employee
            // Join Schedule with ShiftTemplate to get shift details
            var shift = await _context.Schedules
                .Where(s => s.EmployeeId == request.UserId)
                .Join(_context.ShiftTemplates,
                      schedule => schedule.ShiftCode,
                      template => template.Code,
                      (schedule, template) => new UserShiftDto
                      {
                          UserId = schedule.EmployeeId,
                          ShiftCode = schedule.ShiftCode,
                          TimeIn = template.TimeIn,
                          TimeOut = template.TimeOut,
                          BreakMinutes = template.BreakMinutes
                      })
                .FirstOrDefaultAsync(cancellationToken);

            return shift;
        }
    }
}
