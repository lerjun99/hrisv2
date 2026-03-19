using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces
{

    public interface ITimeEntryRepository
    {
        Task<TimeEntry?> GetOpenEntry(string userName, int UserId);
        Task<List<TimeEntry>> GetUserEntries(int UserId);
        Task Add(TimeEntry entry);
        Task Update(TimeEntry entry);
        Task<TimeEntry?> GetActiveEntryAsync(string userName);
    }
}
