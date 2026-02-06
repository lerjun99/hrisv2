using HRIS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Commands
{
    public record StartBreakCommand(string UserName, BreakType Type) : IRequest<Unit>;
}
