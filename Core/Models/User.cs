using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User : CommonModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        public bool EmailVerify { get; set; } = false;
        [Required]
        [MaxLength(20)]
        public string Mobile { get; set; } = null!;
        public bool MobileVerify { get; set; } = false;
        public string Password { get; set; } = null!;
        public int PassError { get; set; } = 0;
        public int BlockCount { get; set; } = 0;
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public int? ProfileRef { get; set; }
        public UserProfile? UserProfile { get; set; }
    }
}
