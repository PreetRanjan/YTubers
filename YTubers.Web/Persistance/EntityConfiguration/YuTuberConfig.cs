using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YTubers.Web.Models;

namespace YTubers.Web.Persistance.EntityConfiguration
{
    public class YuTuberConfig : IEntityTypeConfiguration<YuTuber>
    {
        public void Configure(EntityTypeBuilder<YuTuber> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ChannelName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.ChannelLink).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Age).IsRequired().HasDefaultValue(0);
            builder.Property(c => c.City).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Country).IsRequired().HasMaxLength(20);
            builder.Property(c => c.CategoryId).IsRequired();
            builder.Property(c => c.IsFeatured).HasDefaultValue(false);
            builder.Property(c => c.Price).IsRequired();
            builder.Property(c => c.Sex).IsRequired();
            builder.Property(c => c.YourDescription).HasMaxLength(500);
            builder.Property(c => c.AppUserId).IsRequired();
        }
    }
}
