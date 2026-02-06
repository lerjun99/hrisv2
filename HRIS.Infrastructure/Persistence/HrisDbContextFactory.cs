using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Persistence
{
    public class HrisDbContextFactory : IDesignTimeDbContextFactory<HrisDbContext>
    {
        public HrisDbContext CreateDbContext(string[] args)
        {
            // Load configuration
            IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(Path.GetPathRoot(Environment.SystemDirectory))
           .AddJsonFile("app/hris_v2/appconfig.json", optional: true, reloadOnChange: true)
           .Build();


            // Build options
            var optionsBuilder = new DbContextOptionsBuilder<HrisDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("HrisV2_ConnectionString"));

            return new HrisDbContext(optionsBuilder.Options);
        }
    }
}
