using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Services
{
    public static class AttendanceCalculator
    {
        public static void Compute(TimeEntry entry, ShiftTemplate shift)
        {
            if (entry.ClockIn == null || entry.ClockOut == null)
                return;

            var grace = TimeSpan.FromMinutes(15);

            var shiftStart = entry.ClockIn.Value.Date + shift.TimeIn;
            var shiftEnd = entry.ClockIn.Value.Date + shift.TimeOut;

            if (shift.TimeOut < shift.TimeIn)
                shiftEnd = shiftEnd.AddDays(1); // Night shift support

            // ======================
            // LATE
            // ======================
            if (entry.ClockIn > shiftStart.Add(grace))
            {
                entry.LateMinutes = entry.ClockIn.Value - shiftStart;
                entry.IsLate = true;
            }
            else
            {
                entry.LateMinutes = TimeSpan.Zero;
                entry.IsLate = false;
            }

            // ======================
            // BREAK COMPUTATION
            // ======================
            var totalBreak =
                GetBreak(entry.Break1In, entry.Break1Out) +
                GetBreak(entry.LunchIn, entry.LunchOut) +
                GetBreak(entry.Break3In, entry.Break3Out);

            var allowedBreak = TimeSpan.FromMinutes(shift.BreakMinutes);

            if (totalBreak > allowedBreak)
                entry.BreakOverMinutes = totalBreak - allowedBreak;
            else
                entry.BreakOverMinutes = TimeSpan.Zero;

            // ======================
            // UNDERTIME
            // ======================
            if (entry.ClockOut < shiftEnd)
            {
                entry.UnderTimeMinutes = shiftEnd - entry.ClockOut.Value;
                entry.IsUnderTime = true;
            }
            else
            {
                entry.UnderTimeMinutes = TimeSpan.Zero;
                entry.IsUnderTime = false;
            }

            // ======================
            // OVERTIME
            // ======================
            if (entry.ClockOut > shiftEnd)
            {
                entry.OvertimeMinutes = entry.ClockOut.Value - shiftEnd;
                entry.IsOvertime = true;
            }
            else
            {
                entry.OvertimeMinutes = TimeSpan.Zero;
                entry.IsOvertime = false;
            }
        }

        private static TimeSpan GetBreak(DateTime? start, DateTime? end)
        {
            if (start == null || end == null)
                return TimeSpan.Zero;

            return end.Value - start.Value;
        }
    }
}
