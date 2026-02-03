//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
using SIGID.Core.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Core.Application.DTO.Account
{
    //task80_edelacruz: DTO respuesta de registro con Id y Email agregados
    public class RegisterResponseDTO : Responses
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string IdentificationNumber { get; set; } = string.Empty;
    }
    //task80_edelacruz: Fin DTO respuesta registro
}
