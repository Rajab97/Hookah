using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class CatheringConfiguration : IEntityTypeConfiguration<Cathering>
    {
        public void Configure(EntityTypeBuilder<Cathering> builder)
        {
            builder.Property(I => I.BaseTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.BaseTitleText).HasMaxLength(500);
            builder.Property(I => I.BaseTitleBoldText).HasMaxLength(500);
            builder.Property(I => I.ImageTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.HowItWorksTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.OrderTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.SelectPackageTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.ImageLP).HasMaxLength(500).IsRequired();
            builder.Property(I => I.ImagePL).HasMaxLength(500).IsRequired();
            builder.Property(I => I.ImageMB).HasMaxLength(500).IsRequired();
        }
    }
}
