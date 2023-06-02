using System;
using System.Collections.Generic;

namespace DL;

public partial class UsuarioProducto
{
    public int IdUsuarioProducto { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
