using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VentaProducto
    {
        public int IdVentaProducto { get; set; }
        public ML.SucursalProducto? SucursalProducto { get; set; }
        public int? Cantidad { get; set; }

        public decimal? total { get; set; }
        public decimal? SubTotal { get; set; }
        public List<object>? VentasProductos { get; set; }
        public ML.Venta? Venta { get; set; }//pendiente
    }
}
