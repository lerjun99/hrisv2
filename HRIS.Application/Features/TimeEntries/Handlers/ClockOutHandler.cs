using HRIS.Application.Common.Helpers;
using HRIS.Application.Common.Interfaces;
using HRIS.Application.Features.TimeEntries.Commands;
using HRIS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Features.TimeEntries.Handlers
{
    public class ClockOutHandler : IRequestHandler<ClockOutCommand, Unit>
    {
        private readonly ITimeEntryRepository _repo;
        private readonly IHrisDbContext _context;

        public ClockOutHandler(
            ITimeEntryRepository repo,
            IHrisDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        public async Task<Unit> Handle(ClockOutCommand request, CancellationToken ct)
        {
            var entry = await _repo.GetOpenEntry(request.UserName, request.UserId);

            if (entry == null)
                throw new Exception("No active time entry found.");

            entry.ClockOut = PhilippineTime.Now;

            // ==============================
            // 1️⃣ Get Today's Schedule
            // ==============================
            var today = entry.ClockIn?.DayOfWeek;

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(x =>
                    x.EmployeeId == request.UserId &&
                    x.Day == today, ct);

            if (schedule == null)
            {
                // No schedule = treat as rest day OT
                entry.IsOvertime = true;
                entry.OvertimeMinutes =
                    entry.ClockOut - entry.ClockIn;
            }
            else
            {
                // ==============================
                // 2️⃣ Get Shift Template
                // ==============================
                var shift = await _context.ShiftTemplates
                    .FirstAsync(x => x.Code == schedule.ShiftCode, ct);

                ComputeAttendance(entry, shift);
            }

            await _repo.Update(entry);

            return Unit.Value;
        }
        private void ComputeAttendance(TimeEntry entry, HRIS.Domain.Entities.ShiftTemplate shift)
        {
            if (entry.ClockIn == null || entry.ClockOut == null)
                return;

            var grace = TimeSpan.FromMinutes(15);

            var shiftStart = entry.ClockIn.Value.Date + shift.TimeIn;
            var shiftEnd = entry.ClockIn.Value.Date + shift.TimeOut;

            if (shift.TimeOut < shift.TimeIn)
                shiftEnd = shiftEnd.AddDays(1); // night shift support

            // ================= LATE =================
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

            // ================= BREAK =================
            var totalBreak =
                GetBreak(entry.Break1In, entry.Break1Out) +
                GetBreak(entry.LunchIn, entry.LunchOut) +
                GetBreak(entry.Break3In, entry.Break3Out);

            var allowedBreak = TimeSpan.FromMinutes(shift.BreakMinutes);

            if (totalBreak > allowedBreak)
                entry.BreakOverMinutes = totalBreak - allowedBreak;
            else
                entry.BreakOverMinutes = TimeSpan.Zero;

            // ================= UNDERTIME =================
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

            // ================= OVERTIME =================
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

        private TimeSpan GetBreak(DateTime? start, DateTime? end)
        {
            if (start == null || end == null)
                return TimeSpan.Zero;

            return end.Value - start.Value;
        }
    }
}
