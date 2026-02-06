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

        public Task<TimeEntry?> GetOpenEntry(string userName) =>
            _db.TimeEntries
               .FirstOrDefaultAsync(x => x.UserName == userName && x.ClockOut == null);

        public Task<List<TimeEntry>> GetUserEntries(string userName) =>
            _db.TimeEntries
               .Where(x => x.UserName == userName)
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
    }
}
