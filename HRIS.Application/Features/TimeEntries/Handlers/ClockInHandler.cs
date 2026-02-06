using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.TimeEntries.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Handlers
{
    public class ClockInHandler : IRequestHandler<ClockInCommand, int>
    {
        private readonly ITimeEntryRepository _repo;

        public ClockInHandler(ITimeEntryRepository repo) => _repo = repo;

        public async Task<int> Handle(ClockInCommand request, CancellationToken ct)
        {
            var entry = new TimeEntry
            {
                UserName = request.UserName,
                ClockIn = DateTime.UtcNow
            };

            await _repo.Add(entry);
            return entry.Id;
        }
    }
}
