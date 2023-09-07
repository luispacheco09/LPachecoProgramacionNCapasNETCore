using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? FechaIngreso{ get; set; }
        public string? CodigoBarras { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Modelo { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public ML.Marca? Marca { get; set; }
        public ML.Proveedor? Proveedor { get; set; }
        public ML.Departamento? Departamento { get; set; }
        public ML.Usuario? Usuario { get; set; }
        public List<object>? Productos { get; set; }
    }
}
