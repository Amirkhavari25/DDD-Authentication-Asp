using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public  class Role : CommonModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RoleAction> RoleActions{ get; set; } = new List<RoleAction>();
    }
}
