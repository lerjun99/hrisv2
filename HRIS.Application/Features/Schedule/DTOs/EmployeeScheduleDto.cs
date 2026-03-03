using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.Schedule.DTOs
{
    public class EmployeeScheduleDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public List<ShiftDto> WeeklyShifts { get; set; }
    }
}
