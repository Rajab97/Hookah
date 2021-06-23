using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(I => I.FormTitle).HasMaxLength(400).IsRequired();
            builder.Property(I => I.ImageTitle).HasMaxLength(100).IsRequired();
            builder.Property(I => I.ImageLP).HasMaxLength(500).IsRequired();
            builder.Property(I => I.ImagePL).HasMaxLength(500).IsRequired();
            builder.Property(I => I.ImageMB).HasMaxLength(500).IsRequired();
        }
    }
}
