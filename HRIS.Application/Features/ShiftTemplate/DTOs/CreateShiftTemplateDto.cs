using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.ShiftTemplate.DTOs
{
    public record CreateShiftTemplateDto(
      string Code,
      string TimeIn,
      string TimeOut,
      int BreakMinutes
  );
}
