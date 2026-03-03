using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.ShiftTemplate.DTOs;
using HRIS.Application.Features.ShiftTemplate.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.ShiftTemplate.Handlers
{
    public class ShiftTemplateQueryHandler :
         IRequestHandler<GetAllShiftTemplatesQuery, List<ShiftTemplateDto>>,
         IRequestHandler<GetShiftTemplateByIdQuery, ShiftTemplateDto>
    {
        private readonly IHrisDbContext _context;

        public ShiftTemplateQueryHandler(IHrisDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShiftTemplateDto>> Handle(GetAllShiftTemplatesQuery request, CancellationToken cancellationToken)
        {
            return await _context.ShiftTemplates
                .Select(t => new ShiftTemplateDto(
                    t.Id,
                    t.Code,
                    t.TimeIn.ToString(@"hh\:mm"),
                    t.TimeOut.ToString(@"hh\:mm"),
                    t.BreakMinutes
                ))
                .ToListAsync(cancellationToken);
        }

        public async Task<ShiftTemplateDto> Handle(GetShiftTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var t = await _context.ShiftTemplates.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (t == null) return null;

            return new ShiftTemplateDto(
                t.Id,
                t.Code,
                t.TimeIn.ToString(@"hh\:mm"),
                t.TimeOut.ToString(@"hh\:mm"),
                t.BreakMinutes
            );
        }
    }
}
