using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.Schedule.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.Handlers
{
    public class CreateWeeklyScheduleCommandHandler
      : IRequestHandler<CreateWeeklyScheduleCommand>
    {
        private readonly IHrisDbContext _context;

        public CreateWeeklyScheduleCommandHandler(IHrisDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            CreateWeeklyScheduleCommand request,
            CancellationToken cancellationToken)
        {
            // Optional: remove existing schedule first
            var existing = _context.Schedules
                .Where(x => x.EmployeeId == request.UserId);

            _context.Schedules.RemoveRange(existing);

            // Create new schedules
            var schedules = request.Shifts.Select(x => new HRIS.Domain.Entities.Schedule
            {
                EmployeeId = request.UserId,
                Day = x.Day,
                ShiftCode = x.ShiftCode
            });

            await _context.Schedules.AddRangeAsync(schedules, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
