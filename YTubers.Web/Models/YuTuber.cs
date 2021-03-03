using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTubers.Web.Models
{
    public class YuTuber
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public string ChannelLink { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public bool IsFeatured { get; set; }
        public double Price { get; set; }
        public string AppUserId { get; set; }
        public string ChannelId { get; set; }
        public ulong SubscribersCount { get; set; }
        public string ThumbnailUrl { get; set; }
        public ulong VideoCount { get; set; }
        public string YourDescription { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime UserSince { get; private set; }
        public Sex Sex { get; set; }
        public ICollection<ReachRequest> ReachRequests { get; set; }
        public YuTuber()
        {
            UserSince = DateTime.Now;
        }
    }
    public enum Sex
    {
        Male,Female
    }
}
