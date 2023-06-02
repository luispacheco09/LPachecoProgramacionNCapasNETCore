using System;
using System.Collections.Generic;

namespace DL;

public partial class ProductoInventario
{
    public int IdProductoInventario { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
