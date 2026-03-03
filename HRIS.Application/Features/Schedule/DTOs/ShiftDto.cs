using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.DTOs
{
    public class ShiftDto
    {
        public int ScheduleId { get; set; }
        public DayOfWeek Day { get; set; }
        public string ShiftCode { get; set; }

        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public int BreakMinutes { get; set; }

        public bool IsWeekend { get; set; }
    }
}
