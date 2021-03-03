using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YTubers.Web.Models
{
    public class ReachRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string YuTuberUserId { get; set; }
        public int YuTuberId { get; set; }
        public YuTuber YuTuber { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
    public enum RequestStatus
    {
        Unknown,Sent,Received
    }
}
