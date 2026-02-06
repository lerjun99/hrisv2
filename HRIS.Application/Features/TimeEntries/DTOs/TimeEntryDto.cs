using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.DTOs
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime? ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
        public DateTime? Break1In { get; set; }
        public DateTime? Break1Out { get; set; }
        public DateTime? LunchIn { get; set; }
        public DateTime? LunchOut { get; set; }
        public DateTime? Break3In { get; set; }
        public DateTime? Break3Out { get; set; }
    }
}
