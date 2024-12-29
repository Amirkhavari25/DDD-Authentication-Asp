using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserProfile : CommonModel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime LastLogin { get; set; }
        public string? Avatar { get; set; }

    }
}
