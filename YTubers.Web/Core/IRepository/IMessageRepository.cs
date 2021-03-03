using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Models;

namespace YTubers.Web.Core.IRepository
{
    public interface IMessageRepository
    {
        Task<Message> CreateMessage(Message message);
        Task<IEnumerable<Message>> GetMessagesById(string Id);
        Task<IEnumerable<Message>> GetMessageThread(string first_id, string sec_id);
        Task<string> GetReceiverName(string id);
    }
}
