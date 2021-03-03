using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Data;
using YTubers.Web.Models;

namespace YTubers.Web.Persistance.Repository
{
    public class YuTuberRepository : IYuTuberRepository
    {
        private readonly ApplicationDbContext db;

        public YuTuberRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateYuTuber(YuTuber yuTuber)
        {
            await db.YouTubers.AddAsync(yuTuber);
            await db.SaveChangesAsync();
        }

        public async Task<string> GetUsername(string id)
        {
            return (await db.YouTubers.SingleOrDefaultAsync(c => c.AppUserId == id)).ChannelId;
        }

        public async Task<YuTuber> GetYuTuber(string username)
        {
            return await db.YouTubers.Include(x=>x.Category).SingleOrDefaultAsync(c => c.ChannelId == username);
        }

        public async Task<YuTuber> GetYuTuberById(string id)
        {
            return await db.YouTubers.Include(x => x.Category).SingleOrDefaultAsync(c => c.AppUserId == id);
        }

        public async Task UpdateYuTuber(YuTuber yuTuber)
        {
            db.Entry(yuTuber).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string id)
        {
            return await db.YouTubers.AnyAsync(a => a.AppUserId == id);
        }
    }
}
