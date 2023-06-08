using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class UsuarioRol
    {
        public Guid UserId { get; set; }
        public Guid RolId { get; set; }
        public List<object> UsuariosRoles { get; set; }

    }
}
