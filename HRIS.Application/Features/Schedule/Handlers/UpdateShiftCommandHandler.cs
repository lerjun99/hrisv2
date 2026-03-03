using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.Schedule.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.Handlers
{
    public class UpdateShiftCommandHandler
     : IRequestHandler<UpdateShiftCommand>
    {
        private readonly IHrisDbContext _context;

        public UpdateShiftCommandHandler(IHrisDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateShiftCommand request,CancellationToken cancellationToken)
        {
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(x => x.Id == request.ScheduleId, cancellationToken);

            if (schedule == null)
                throw new Exception("Schedule not found");

            schedule.ShiftCode = request.NewShiftCode;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
