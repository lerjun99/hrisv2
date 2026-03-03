using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.DTOs
{
    public class UserShiftDto
    {
        public int UserId { get; set; }
        public string ShiftCode { get; set; } = string.Empty;
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public int BreakMinutes { get; set; }
    }
}
