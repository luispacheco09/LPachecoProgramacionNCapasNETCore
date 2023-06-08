using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.UsuarioRol UsuarioRol { get; set; }

        public DL.AspNetUser AspNetUser { get; set; }
        public DL.AspNetRole AspNetRole { get; set; }


    }
}
