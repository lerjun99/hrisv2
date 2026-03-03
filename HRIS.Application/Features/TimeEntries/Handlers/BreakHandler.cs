using HRIS.Application.Common.Helpers;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.TimeEntries.Commands;
using HRIS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Handlers
{
    public class BreakHandler :
      IRequestHandler<StartBreakCommand, Unit>,
      IRequestHandler<EndBreakCommand, Unit>
    {
        private readonly ITimeEntryRepository _repo;

        public BreakHandler(ITimeEntryRepository repo) => _repo = repo;

        public async Task<Unit> Handle(StartBreakCommand request, CancellationToken ct)
        {
            var entry = await _repo.GetOpenEntry(request.UserName , request.UserId);

            if (request.Type == BreakType.Morning)
                entry!.Break1In = PhilippineTime.Now;
            if (request.Type == BreakType.Lunch)
                entry!.LunchIn = PhilippineTime.Now;
            if (request.Type == BreakType.Afternoon)
                entry!.Break3In = PhilippineTime.Now;

            await _repo.Update(entry!);
            return Unit.Value;
        }

        public async Task<Unit> Handle(EndBreakCommand request, CancellationToken ct)
        {
            var entry = await _repo.GetOpenEntry(request.UserName, request.UserId);

            if (request.Type == BreakType.Morning)
                entry!.Break1Out = PhilippineTime.Now;
            if (request.Type == BreakType.Lunch)
                entry!.LunchOut = PhilippineTime.Now;
            if (request.Type == BreakType.Afternoon)
                entry!.Break3Out = PhilippineTime.Now;

            await _repo.Update(entry!);
            return Unit.Value;
        }
    }
}