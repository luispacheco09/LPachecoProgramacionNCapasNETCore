using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    var listaProveedor = (from proveedorDL in context.Proveedors
                                          select new
                                          {
                                              IdProveedor = proveedorDL.IdProveedor,
                                              Nombre = proveedorDL.Nombre,
                                              Direccion = proveedorDL.Direccion,
                                              Telefono = proveedorDL.Telefono,
                                          }).ToList();

                    if (listaProveedor != null && listaProveedor.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in listaProveedor)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();
                            proveedor.IdProveedor = obj.IdProveedor;
                            proveedor.Nombre = obj.Nombre;

                            result.Objects.Add(proveedor);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La tabala no tiene datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
