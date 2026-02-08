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
    public class GetActiveTimeEntryHandler
       : IRequestHandler<GetActiveTimeEntryQuery, TimeEntryDto?>
    {
        private readonly ITimeEntryRepository _repository;

        public GetActiveTimeEntryHandler(ITimeEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<TimeEntryDto?> Handle(
            GetActiveTimeEntryQuery request,
            CancellationToken cancellationToken)
        {
            var entry = await _repository.GetActiveEntryAsync(request.UserName);

            if (entry == null)
                return null;

            return new TimeEntryDto
            {
                UserName = entry.UserName,
                ClockIn = entry.ClockIn,
                ClockOut = entry.ClockOut,
                Break1In = entry.Break1In,
                Break1Out = entry.Break1Out,
                LunchIn = entry.LunchIn,
                LunchOut = entry.LunchOut,
                Break3In = entry.Break3In,
                Break3Out = entry.Break3Out
            };
        }
    }
}
