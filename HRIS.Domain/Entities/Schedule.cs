using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }   // FK to Employee

        public DayOfWeek Day { get; set; }

        public string ShiftCode { get; set; }

        public Employee Employee { get; set; }
    }
}
