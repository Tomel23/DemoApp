using Demo.Infrastructure.Database.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Demo.Infrastructure.Database
{
    public class DemoContext : DbContext
    {
        public DemoContext()
        {

        }

        protected readonly IConfiguration Configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "demoDb.db"));
        }

        public DbSet<AuthenticationToken> DbTokens => Set<AuthenticationToken>();
    }
}
