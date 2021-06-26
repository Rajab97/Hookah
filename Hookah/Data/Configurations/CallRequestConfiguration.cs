using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class CallRequestConfiguration : IEntityTypeConfiguration<CallRequest>
    {
        public void Configure(EntityTypeBuilder<CallRequest> builder)
        {
            builder.Property(I => I.Email).HasMaxLength(100);
            builder.Property(I => I.EventDate);
            builder.Property(I => I.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(I => I.PhoneNumber).HasMaxLength(25).IsRequired();
            builder.Property(I => I.Message).HasMaxLength(1000);
            builder.Property(I => I.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(I => I.LastName).HasMaxLength(50).IsRequired();
        }
    }
}
