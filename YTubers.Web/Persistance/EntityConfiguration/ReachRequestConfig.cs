using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Models;

namespace YTubers.Web.Persistance.EntityConfiguration
{
    public class ReachRequestConfig : IEntityTypeConfiguration<ReachRequest>
    {
        public void Configure(EntityTypeBuilder<ReachRequest> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.FullName).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Email).IsRequired().HasMaxLength(50);
            builder.Property(r => r.City).IsRequired().HasMaxLength(30);
            builder.Property(r => r.State).IsRequired().HasMaxLength(30);
            builder.Property(r => r.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Message).IsRequired().HasMaxLength(250);
            builder.Property(r => r.AppUserId).IsRequired();
            builder.Property(r => r.YuTuberId).IsRequired();
            builder.Property(r => r.YuTuberUserId).IsRequired();
        }
    }
}
