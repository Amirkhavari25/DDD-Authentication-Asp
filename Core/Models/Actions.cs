using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Actions : CommonModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<RoleAction> RoleActions { get; set; } = new List<RoleAction>();
    }
}
