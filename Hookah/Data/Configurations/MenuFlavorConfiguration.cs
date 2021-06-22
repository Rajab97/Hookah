using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class MenuFlavorConfiguration : IEntityTypeConfiguration<MenuFlavor>
    {
        public void Configure(EntityTypeBuilder<MenuFlavor> builder)
        {
            builder.Property(I => I.Text).HasMaxLength(200).IsRequired();
        }
    }
}
