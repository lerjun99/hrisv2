using HRIS.Application.Common.Helpers;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.TimeEntries.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Handlers
{
    public class ClockOutHandler : IRequestHandler<ClockOutCommand, Unit>
    {
        private readonly ITimeEntryRepository _repo;

        public ClockOutHandler(ITimeEntryRepository repo) => _repo = repo;

        public async Task<Unit> Handle(ClockOutCommand request, CancellationToken ct)
        {
            var entry = await _repo.GetOpenEntry(request.UserName);
            entry!.ClockOut = PhilippineTime.Now;
            await _repo.Update(entry);
            return Unit.Value;
        }
    }
}
