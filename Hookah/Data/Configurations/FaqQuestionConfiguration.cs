using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class FaqQuestionConfiguration : IEntityTypeConfiguration<FaqQuestion>
    {
        public void Configure(EntityTypeBuilder<FaqQuestion> builder)
        {
            builder.Property(I => I.Question).HasMaxLength(1000).IsRequired();
            builder.Property(I => I.Question).HasMaxLength(2500).IsRequired();
        }
    }
}
