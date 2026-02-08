using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Commands
{
    public record ClockInCommand(int UserId,string UserName) : IRequest<int>;
}
