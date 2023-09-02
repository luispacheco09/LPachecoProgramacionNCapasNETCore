﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class SucursalProducto
{
    public int IdSucursalProducto { get; set; }

    public int? IdProducto { get; set; }

    public int? IdSucursal { get; set; }

    public int? Stock { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; }
}
