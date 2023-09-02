using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Curp { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string EmailEmpresarial { get; set; }

        public string Sexo { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public string FechaNacimiento { get; set; }

        public string FechaRegistro { get; set; }

        public byte[] FotoRegistro { get; set; }

        public int IdArea { get; set; }

        public int IdUsuarioModificacion { get; set; }


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
