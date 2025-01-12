using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TokenPayload
    {
        public string Username { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
