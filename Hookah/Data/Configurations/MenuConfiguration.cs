using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(I => I.FlavorsText).HasMaxLength(1000).IsRequired();
            builder.Property(I => I.FruitHeadText).HasMaxLength(1000).IsRequired();
            builder.Property(I => I.ImageTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.FruitHeadTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.FlavorsTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.ImageLP).HasMaxLength(500).IsRequired();
            builder.Property(I => I.ImagePL).HasMaxLength(500).IsRequired();
            builder.Property(I => I.ImageMB).HasMaxLength(500).IsRequired();
        }
    }
}
