using SIGID.Core.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Core.Application.DTO.Account
{
    public class LoginResponseDTO : Responses
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsActive { get; set; }
        public Object token { get; set; }
    }
}
