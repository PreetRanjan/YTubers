using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Data;

namespace YTubers.Web.Utility
{
    public interface IDbInitializer
    {
        Task SeedAsync();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext db;
        private readonly RoleManager<IdentityRole> roleManager;
        

        public DbInitializer(ApplicationDbContext db,RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.roleManager = roleManager;
            
        }
        public async Task SeedAsync()
        {
            try
            {
                if (db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
            }
            catch (Exception)
            {
                
            }

            //if (db.Roles.Any(r => r.Name == RoleNames.Admin)) return;
            if (!await roleManager.RoleExistsAsync(RoleNames.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = RoleNames.Admin });
            }
            if (!await roleManager.RoleExistsAsync(RoleNames.User))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = RoleNames.User });
            }
            if (!await roleManager.RoleExistsAsync(RoleNames.Mod))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = RoleNames.Mod });
            }
        }
    }
}
