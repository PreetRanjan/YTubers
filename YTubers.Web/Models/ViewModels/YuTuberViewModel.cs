using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YTubers.Web.Models.ViewModels
{
    public class YuTuberViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string ChannelName { get; set; }
        [Required]
        [MaxLength(150)]
        public string ChannelLink { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Range(10,89)]
        public int Age { get; set; }
        public bool IsFeatured { get; set; }
        [Required]
        [Range(0,1000000)]
        public double Price { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [Required]
        public string ChannelId { get; set; }
        [Required]
        public ulong SubscribersCount { get; set; }
        [Required]
        public string ThumbnailUrl { get; set; }
        public ulong VideoCount { get; set; }
        [Required]
        public string YourDescription { get; set; }
        public DateTime UserSince { get; private set; }
        public Sex Sex { get; set; }
        public YuTuberViewModel()
        {
            UserSince = DateTime.Now;
        }
    }
    public class YuTuberViewModel2
    {
        public int Id { get; set; }
        public string YuTuberUserId { get; set; }
        public string ChannelName { get; set; }
     
        public string ChannelLink { get; set; }
    
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
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
        public DateTime UserSince { get; private set; }
        public Sex Sex { get; set; }
        public ReachRequestViewModel ReachModel { get; set; }
        public YuTuberViewModel2()
        {
            UserSince = DateTime.Now;
            ReachModel = new ReachRequestViewModel();
        }
    }
}
