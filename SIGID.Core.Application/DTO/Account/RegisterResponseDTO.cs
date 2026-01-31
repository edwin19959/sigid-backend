using SIGID.Core.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Core.Application.DTO.Account
{
    public class RegisterResponseDTO : Responses
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string IdentificationNumber { get; set; }
    }
}
