using HRIS.Application.Common.Interfaces;
using HRIS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Persistence
{
    public class HrisDbContext : DbContext, IHrisDbContext
    {
        public HrisDbContext(DbContextOptions<HrisDbContext> options) : base(options) { }
        public DbSet<FaceProfile> FaceProfiles { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<ApiTokenModel> ApiTokenModels { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
