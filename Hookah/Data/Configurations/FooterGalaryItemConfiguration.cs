using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class FooterGalaryItemConfiguration : IEntityTypeConfiguration<FooterGalaryItem>
    {
        public void Configure(EntityTypeBuilder<FooterGalaryItem> builder)
        {
            builder.Property(m => m.ImagePath).HasMaxLength(500);
            builder.Property(m => m.Text).HasMaxLength(500);
        }
    }
}
