using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserModel
    {

        public string Username { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool EmailVerify { get; set; }

        public string Mobile { get; set; } = null!;

        public bool MobileVerify { get; set; }

        public string Password { get; set; } = null!;

        public int PassError { get; set; }

        public int BlockCount { get; set; }

        public string Avatar { get; set; } = null!;

        public string AllAction { get; set; } = null!;

        public string Action { get; set; } = null!;

        public string Rolles { get; set; } = null!;

        public string Groups { get; set; } = null!;

        public string? Token { get; set; }

        public DateTime? LastLogin { get; set; }

        public string? Status { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public string ModifyBy { get; set; } = null!;

        public DateTime ModifyDate { get; set; }
    }
}
