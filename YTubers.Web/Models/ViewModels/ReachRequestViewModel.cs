using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YTubers.Web.Models.ViewModels
{
    public class ReachRequestViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        [Required]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(250)]
        public string Message { get; set; }
        public string YuTuberId { get; set; }
        public YuTuber YuTuber { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
