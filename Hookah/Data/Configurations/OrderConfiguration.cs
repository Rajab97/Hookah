using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(I => I.Additions).HasMaxLength(2000);
            builder.Property(I => I.Address).HasMaxLength(1000);
            builder.Property(I => I.Email).HasMaxLength(100);
            builder.Property(I => I.EventDate).IsRequired();
            builder.Property(I => I.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(I => I.HourseOfService);
            builder.HasOne(m=>m.Package).WithMany(m=>m.Orders).OnDelete(DeleteBehavior.SetNull);
            builder.Property(I => I.PhoneNumber).HasMaxLength(25).IsRequired();
        }
    }
}
