using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class CatheringEventConfiguration : IEntityTypeConfiguration<CatheringEvent>
    {
        public void Configure(EntityTypeBuilder<CatheringEvent> builder)
        {
            builder.Property(I => I.Name).HasMaxLength(200).IsRequired();
        }
    }
}
