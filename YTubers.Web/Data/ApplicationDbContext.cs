using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YTubers.Web.Models;
using YTubers.Web.Persistance.EntityConfiguration;

namespace YTubers.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<YuTuber> YouTubers { get; set; }
        public DbSet<ReachRequest> ReachRequests { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new YuTuberConfig());
            builder.ApplyConfiguration(new ReachRequestConfig());
            builder.ApplyConfiguration(new MessageConfig());

            //builder.Entity<Message>().HasOne(o => o.Sender)
            //    .WithMany(o => o.Messages)
            //    .HasForeignKey(o => o.SenderId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //builder.Entity<Message>().HasOne<AppUser>(c => c.Sender)
            //    .WithMany(x => x.Messages).HasForeignKey(c => c.SenderId);
            base.OnModelCreating(builder);
        }
    }
}
