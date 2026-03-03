using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public DateTime? ClockIn { get; set; }
        public DateTime? Break1In { get; set; }
        public DateTime? Break1Out { get; set; }
        public DateTime? LunchIn { get; set; }
        public DateTime? LunchOut { get; set; }
        public DateTime? Break3In { get; set; }
        public DateTime? Break3Out { get; set; }
        public DateTime? ClockOut { get; set; }
        public TimeSpan? LateMinutes { get; set; }
        public TimeSpan? UnderTimeMinutes { get; set; }
        public TimeSpan? OvertimeMinutes { get; set; }
        public TimeSpan? BreakOverMinutes { get; set; }
        public bool IsLate { get; set; }
        public bool IsUnderTime { get; set; }
        public bool IsOvertime { get; set; }
        // ----------------- Computed Property -----------------
        public double TotalWorkedHours
        {
            get
            {
                if (ClockIn == null || ClockOut == null)
                    return 0;

                // Total time worked (ClockOut - ClockIn)
                var total = ClockOut.Value - ClockIn.Value;

                // Subtract breaks
                total -= GetBreakDuration(Break1In, Break1Out);
                total -= GetBreakDuration(LunchIn, LunchOut);
                total -= GetBreakDuration(Break3In, Break3Out);

                return total.TotalHours;
            }

        }

        private TimeSpan GetBreakDuration(DateTime? start, DateTime? end)
        {
            if (start == null || end == null)
                return TimeSpan.Zero;

            return end.Value - start.Value;
        }
    }
}
