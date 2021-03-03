using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YTubers.Web.Models.ViewModels
{
    public class MessageViewModel
    {
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }
        [Required]
        public string ReceiverId { get; set; }
        [Required]
        public string SenderName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
        public string ReceiverName { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public MessageViewModel()
        {
            When = DateTime.Now;
        }
    }
}
