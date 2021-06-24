using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class HomeLinkConfiguration : IEntityTypeConfiguration<HomeLink>
    {
        public void Configure(EntityTypeBuilder<HomeLink> builder)
        {
            builder.Property(m => m.ImagePath).HasMaxLength(500);
            builder.Property(m => m.Name).HasMaxLength(50);
            builder.Property(m => m.Link).HasMaxLength(500);
            builder.Property(m => m.ButtonText).HasMaxLength(50);
        }
    }
}
