using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.DTOs
{
    public class CreateShiftDto
    {
        public DayOfWeek Day { get; set; }
        public string ShiftCode { get; set; }
    }
}
