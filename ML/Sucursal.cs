using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Sucursal
    {
        public int IdSucursal { get; set; }

        public string? Nombre { get; set; }

        public string? Calle { get; set; }

        public string? NumeroInterior { get; set; }

        public string? NumeroExterior { get; set; }

        public string? CP { get; set; }

        public string? Colonia { get; set; }

        public string? Municipio { get; set; }

        public string? Estado { get; set; }

        public string? PaginaWeb { get; set; }
        public List<object>? Sucursales { get; set; }
    }
}
