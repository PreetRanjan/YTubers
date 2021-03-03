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
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext db;

        public MessageRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Message> CreateMessage(Message message)
        {
            await db.Messages.AddAsync(message);
            await db.SaveChangesAsync();
            return await Task.FromResult(message);
        }

        public async Task<IEnumerable<Message>> GetMessagesById(string Id)
        {
            return await db.Messages.Where(c => c.SenderId == Id || c.Receiverid == Id).ToListAsync();
        }
        public async Task<IEnumerable<Message>> GetMessageThread(string first_id,string sec_id)
        {
            var messages = await db.Messages.Where(c => c.SenderId == first_id || c.Receiverid == first_id)
                .OrderBy(c => c.When).ToListAsync();
            return messages;
        }
        public async Task<string> GetReceiverName(string id)
        {
            return ((await db.Users.SingleOrDefaultAsync(X => X.Id == id)).Email).Split('@')[0].ToCamelCase();
        }
    }
}
