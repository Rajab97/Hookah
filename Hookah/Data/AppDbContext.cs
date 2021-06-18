using Hookah.Abstracts;
using Hookah.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Hookah.Data
{
    public class AppDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageItem> PackageItems { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                var model = entity.Entity as BaseEntity;
                if (model == null)
                    continue;

                if (entity.State == EntityState.Added)
                {
                    model.AddedDate = DateTime.Now;
                    model.Version = 1;
                }
                else if (entity.State == EntityState.Modified)
                {
                    model.UpdatedDate = DateTime.Now;
                    model.Version++;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
/*
            foreach (var entity in GetType().Assembly.GetTypes().Where(m => typeof(BaseEntity).IsAssignableFrom(m)))
            {
                builder.Entity(entity).Property(nameof(BaseEntity.Version)).HasDefaultValue(1);
            }*/
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }
    }
}
