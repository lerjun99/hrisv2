using Emgu.CV.Ocl;
using HRIS.Application.Common.Interfaces;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Persistence.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly HrisDbContext _db;

        public TimeEntryRepository(HrisDbContext db) => _db = db;

        public Task<TimeEntry?> GetOpenEntry(string userName , int UserId) =>
            _db.TimeEntries
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.UserName == userName && x.ClockOut == null);

        public Task<List<TimeEntry>> GetUserEntries(int UserId) =>
            _db.TimeEntries
               .AsNoTracking()
               .Where(x => x.UserId == UserId)
               .OrderByDescending(x => x.ClockIn)
               .ToListAsync();

        public async Task Add(TimeEntry entry)
        {
            _db.TimeEntries.Add(entry);
            await _db.SaveChangesAsync();
        }

        public async Task Update(TimeEntry entry)
        {
            _db.TimeEntries.Update(entry);
            await _db.SaveChangesAsync();
        }
        public async Task<TimeEntry?> GetActiveEntryAsync(string userName)
        {
            var startOfDay = DateTime.Today;          // 2026-02-08 00:00:00.0000000
            var endOfDay = startOfDay.AddDays(1);     // 2026-02-09 00:00:00.0000000

            return await _db.TimeEntries
                .AsNoTracking()
                .Where(x =>
                    x.UserName == userName &&
                    x.ClockOut == null &&
                    x.ClockIn >= startOfDay &&
                    x.ClockIn < endOfDay
                )
                .OrderByDescending(x => x.ClockIn)
                .FirstOrDefaultAsync();
        }
    }
}
