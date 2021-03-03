using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using YTubers.Web.Core.IRepository;
using YTubers.Web.Data;
using YTubers.Web.Models;

namespace YTubers.Web.Persistance.Repository
{
    public class ReachRepository : IReachRepository
    {
        private readonly ApplicationDbContext db;

        public ReachRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task SendRequest(ReachRequest request)
        {
            await db.ReachRequests.AddAsync(request);
            await db.SaveChangesAsync();
        }
        public async Task<IEnumerable<ReachRequest>> GetRequestsById(string id,int page)
        {
            return await db.ReachRequests.Where(c => c.YuTuberUserId == id).ToPagedListAsync(page,10);
        }
        public async Task<IEnumerable<ReachRequest>> GetSentRequestsById(string id, int page)
        {
            return await db.ReachRequests.Where(c => c.AppUserId == id && c.RequestStatus == RequestStatus.Sent).ToPagedListAsync(page, 10);
        }
        //Mark Message as Received
        public async Task MarkMessagesReceived(string id)
        {
            var unreadMessages = await db.ReachRequests.Where(c => c.RequestStatus != RequestStatus.Received).ToListAsync();
            unreadMessages.ForEach(req => 
            { 
                req.RequestStatus = RequestStatus.Received; 
            }); 

        }
    }
}
