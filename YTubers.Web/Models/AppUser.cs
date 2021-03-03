using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YTubers.Web.Models
{
    public class AppUser:IdentityUser
    {
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }
        public Gender Gender { get; set; }
        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }
        public ICollection<ReachRequest> ReachRequests { get; set; }

        //1-* AppUser-Messages
        public ICollection<Message> Messages { get; set; }
    }
    public enum Gender
    {
        [Description("Male")]
        Male,
        [Description("Female")]
        Female
    }
}
