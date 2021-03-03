using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Models;

namespace YTubers.Web.Persistance.EntityConfiguration
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(x => x.When).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.Receiverid).IsRequired();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SenderName).IsRequired().HasMaxLength(128);
        }
    }
}
