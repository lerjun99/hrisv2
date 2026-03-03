using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.ShiftTemplate.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace HRIS.Application.Features.ShiftTemplate.Handlers
{
    public class ShiftTemplateHandlers :
        IRequestHandler<CreateShiftTemplateCommand, int>,     // returns new Id
        IRequestHandler<UpdateShiftTemplateCommand, Unit>,  // no value returned
        IRequestHandler<DeleteShiftTemplateCommand, Unit>  
    {
        private readonly IHrisDbContext _context;

        public ShiftTemplateHandlers(IHrisDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateShiftTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = new HRIS.Domain.Entities.ShiftTemplate
            {
                Code = request.Template.Code,
                TimeIn = TimeSpan.Parse(request.Template.TimeIn),
                TimeOut = TimeSpan.Parse(request.Template.TimeOut),
                BreakMinutes = request.Template.BreakMinutes
            };

            _context.ShiftTemplates.Add(template);
            await _context.SaveChangesAsync(cancellationToken);

            return template.Id;
        }

        public async Task<Unit> Handle(UpdateShiftTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _context.ShiftTemplates
                .FirstOrDefaultAsync(t => t.Id == request.Template.Id, cancellationToken);

            if (template == null)
                throw new Exception("Shift template not found");

            template.Code = request.Template.Code;
            template.TimeIn = TimeSpan.Parse(request.Template.TimeIn);
            template.TimeOut = TimeSpan.Parse(request.Template.TimeOut);
            template.BreakMinutes = request.Template.BreakMinutes;
            template.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteShiftTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _context.ShiftTemplates
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (template == null)
                throw new Exception("Shift template not found");

            _context.ShiftTemplates.Remove(template);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
