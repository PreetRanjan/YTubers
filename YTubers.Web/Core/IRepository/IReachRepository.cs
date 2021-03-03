using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Models;

namespace YTubers.Web.Core.IRepository
{
    public interface IReachRepository
    {
        Task SendRequest(ReachRequest request);
        Task<IEnumerable<ReachRequest>> GetRequestsById(string id,int page);
        Task<IEnumerable<ReachRequest>> GetSentRequestsById(string id, int page);
        Task MarkMessagesReceived(string id);
    }
}
