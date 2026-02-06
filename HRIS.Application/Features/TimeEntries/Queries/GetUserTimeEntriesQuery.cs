using HRIS.Application.Features.TimeEntries.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Queries
{
    public record GetUserTimeEntriesQuery(string UserName)
        : IRequest<List<TimeEntryDto>>;
}
