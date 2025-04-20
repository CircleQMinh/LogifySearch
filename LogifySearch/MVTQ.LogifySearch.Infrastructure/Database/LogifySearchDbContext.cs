using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVTQ.LogifySearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Infrastructure.Database
{
    public class LogifySearchDbContext : IdentityDbContext<User>
    {
        public LogifySearchDbContext(DbContextOptions<LogifySearchDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //modelBuilder.ApplyConfiguration(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
            // Remove aspnet from table name when using identityuser
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (!String.IsNullOrEmpty(tableName))
                {
                    if (tableName.StartsWith("AspNet"))
                    {
                        entityType.SetTableName(tableName.Substring(6));
                    }
                }

            }
            //builder.ApplyConfiguration(new RoleConfiguration());
        }

        public virtual Task<int> SaveChangesAsync(string username = "")
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.UpdatedDate = DateTime.Now;
                entry.Entity.UpdatedBy = username;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = username;
                }
            }
            return base.SaveChangesAsync();
        }

    }
}
