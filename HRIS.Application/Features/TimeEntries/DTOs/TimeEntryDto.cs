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
        public string UserName { get; set; } = string.Empty;
        public DateTime? ClockIn { get; set; }
        public DateTime? Break1In { get; set; }
        public DateTime? Break1Out { get; set; }
        public TimeSpan? Break1Duration => Break1Out.HasValue && Break1In.HasValue ? Break1Out - Break1In : null;
        public DateTime? LunchIn { get; set; }
        public DateTime? LunchOut { get; set; }
        public TimeSpan? LunchDuration => LunchOut.HasValue && LunchIn.HasValue ? LunchOut - LunchIn : null;
        public DateTime? Break3In { get; set; }
        public DateTime? Break3Out { get; set; }
        public TimeSpan? Break3Duration => Break3Out.HasValue && Break3In.HasValue ? Break3Out - Break3In : null;
        public DateTime? ClockOut { get; set; }
        public TimeSpan? TotalDuration
        {
            get
            {
                if (!ClockIn.HasValue || !ClockOut.HasValue) return null;
                var total = ClockOut.Value - ClockIn.Value;
                if (Break1Duration.HasValue) total -= Break1Duration.Value;
                if (LunchDuration.HasValue) total -= LunchDuration.Value;
                if (Break3Duration.HasValue) total -= Break3Duration.Value;
                return total;
            }
        }
    }
}
