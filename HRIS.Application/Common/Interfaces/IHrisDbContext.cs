using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Interfaces
{
    public interface IHrisDbContext
    {
        DbSet<FaceProfile> FaceProfiles { get; }
        DbSet<UserAccount> UserAccounts { get; }
        DbSet<UploadedFile> UploadedFiles { get; }
        DbSet<ApiTokenModel> ApiTokenModels { get; }
        DbSet<TimeEntry> TimeEntries { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
