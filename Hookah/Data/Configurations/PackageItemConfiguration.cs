using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class PackageItemConfiguration : IEntityTypeConfiguration<PackageItem>
    {
        public void Configure(EntityTypeBuilder<PackageItem> builder)
        {
            builder.ToTable("PackageItems");
            builder.Property(m => m.Text).HasMaxLength(250);
            builder.HasOne(m => m.Package).WithMany(m => m.Items);
        }
    }
}
