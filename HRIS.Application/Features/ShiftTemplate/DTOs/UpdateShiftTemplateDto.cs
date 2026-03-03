using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.ShiftTemplate.DTOs
{
    public record UpdateShiftTemplateDto(
           int Id,
           string Code,
           string TimeIn,
           string TimeOut,
           int BreakMinutes
       );
}
