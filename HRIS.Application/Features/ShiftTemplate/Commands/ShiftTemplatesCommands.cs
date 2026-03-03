using HRIS.Application.Features.ShiftTemplate.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.ShiftTemplate.Commands
{
    // Create returns Id
    public record CreateShiftTemplateCommand(CreateShiftTemplateDto Template) : IRequest<int>;

    // Update returns Unit (no value)
    public record UpdateShiftTemplateCommand(UpdateShiftTemplateDto Template) : IRequest<Unit>;

    // Delete returns Unit (no value)
    public record DeleteShiftTemplateCommand(int Id) : IRequest<Unit>;
}
