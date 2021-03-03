using System;

namespace YTubers.Web.Models
{
    public class Message
    {
        public long Id { get; set; }

        public string SenderId { get; set; }
        public string Receiverid { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public Message()
        {
            When = DateTime.Now;
        }
    }
}
