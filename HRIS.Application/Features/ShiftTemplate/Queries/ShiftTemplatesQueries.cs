using HRIS.Application.Features.ShiftTemplate.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.ShiftTemplate.Queries
{
    public record GetAllShiftTemplatesQuery() : IRequest<List<ShiftTemplateDto>>;
    public record GetShiftTemplateByIdQuery(int Id) : IRequest<ShiftTemplateDto>;
}
