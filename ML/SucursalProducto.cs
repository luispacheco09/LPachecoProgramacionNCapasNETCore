using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class SucursalProducto
    {
        public int IdSucursalProducto { get; set; }

        public int? IdProducto { get; set; }

        public int? IdSucursal { get; set; }

        public int? Stock { get; set; }
        public ML.Producto? Producto { get; set; }
        public ML.Sucursal? Sucursal { get; set; }
        public List<object>? SucuralesProductos { get; set; }
    }
}
