using Hookah.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah.Data.Configurations
{
    public class SiteConfigurationConfiguration : IEntityTypeConfiguration<SiteConfiguration>
    {
        public void Configure(EntityTypeBuilder<SiteConfiguration> builder)
        {
            builder.Property(m => m.PhoneNumberForCall).HasMaxLength(25).IsRequired();

            builder.Property(I => I.InstagramProfileName).HasMaxLength(50);
            builder.Property(I => I.InstagramLink).HasMaxLength(250);
            builder.Property(I => I.FacebookLink).HasMaxLength(250);
            builder.Property(I => I.YoutubeLink).HasMaxLength(250);
            builder.Property(I => I.TwitterLink).HasMaxLength(250);

            builder.Property(m => m.CompanyName).HasMaxLength(50).IsRequired();
            builder.Property(I => I.CallButtonText).HasMaxLength(50).IsRequired();
            builder.Property(m => m.RequestCallButton).HasMaxLength(50).IsRequired();
            builder.Property(I => I.CompanyLogoImage).HasMaxLength(500).IsRequired();
        }
    }
}
