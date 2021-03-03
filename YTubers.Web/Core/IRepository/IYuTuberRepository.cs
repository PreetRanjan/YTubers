using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Models;

namespace YTubers.Web.Core.IRepository
{
    public interface IYuTuberRepository
    {
        Task CreateYuTuber(YuTuber yuTuber);
        Task<bool> UserExists(string id);
        Task<YuTuber> GetYuTuber(string id);
        Task<YuTuber> GetYuTuberById(string id);
        Task UpdateYuTuber(YuTuber yuTuber);
        Task<string> GetUsername(string id);
    }
}
