using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data
{
    public class GeneralDbContext:DbContext
    {
        public DbSet<OrderGridViewModel> OrderGridView { get; set; }
        public DbSet<CallRequestGridViewModel> CallRequestGridView { get; set; }
        public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
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
            builder.Entity<OrderGridViewModel>().ToView("OrdersGridView");
            builder.Entity<CallRequestGridViewModel>().ToView("CallRequestsGridView");
        }
    }
}
