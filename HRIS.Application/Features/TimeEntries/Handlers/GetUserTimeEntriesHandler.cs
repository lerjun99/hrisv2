using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.TimeEntries.DTOs;
using HRIS.Application.Features.TimeEntries.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Handlers
{
    public class GetUserTimeEntriesHandler
        : IRequestHandler<GetUserTimeEntriesQuery, List<TimeEntryDto>>
    {
        private readonly ITimeEntryRepository _repo;

        public GetUserTimeEntriesHandler(ITimeEntryRepository repo)
            => _repo = repo;

        public async Task<List<TimeEntryDto>> Handle(
            GetUserTimeEntriesQuery request, CancellationToken ct)
        {
            return (await _repo.GetUserEntries(request.UserId))
                .Select(e => new TimeEntryDto
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    ClockIn = e.ClockIn,
                    ClockOut = e.ClockOut,
                    Break1In = e.Break1In,
                    Break1Out = e.Break1Out,
                    LunchIn = e.LunchIn,
                    LunchOut = e.LunchOut,
                    Break3In = e.Break3In,
                    Break3Out = e.Break3Out
                }).ToList();
        }
    }
}
