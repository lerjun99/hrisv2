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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ShiftTemplate> ShiftTemplates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
            .HasMany(e => e.Schedules)
            .WithOne(s => s.Employee)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ShiftTemplate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TimeIn).IsRequired();
                entity.Property(e => e.TimeOut).IsRequired();
                entity.Property(e => e.BreakMinutes).IsRequired();
            });
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
