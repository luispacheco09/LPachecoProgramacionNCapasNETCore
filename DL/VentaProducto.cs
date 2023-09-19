using System;
using System.Collections.Generic;

namespace DL;

public partial class VentaProducto
{
    public int IdVentaProducto { get; set; }

    public int? IdVenta { get; set; }

    public int? IdSucursalProducto { get; set; }

    public int? Cantidad { get; set; }

    public virtual SucursalProducto? IdSucursalProductoNavigation { get; set; }

    public virtual Ventum? IdVentaNavigation { get; set; }
}
