using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Venta
    {
        public static ML.Result Add(string IdUser, decimal? Total)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    DL.Ventum DLVenta = new DL.Ventum();
                    DLVenta.IdUser = IdUser;
                    DLVenta.Total = Total;
                    DLVenta.IdMetodoPago = 1;
                    DLVenta.Fecha = DateTime.Now;

                    context.Venta.Add(DLVenta);

                    result.Object = DLVenta;

                    int RowsAffected = context.SaveChanges();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar la venta";
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

        public static ML.Result AddVentaProducto(int? IdVenta, int? IdSucursalProducto, int? cantidad)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LpachecoProgramacionNcapasNetcoreContext context = new DL.LpachecoProgramacionNcapasNetcoreContext())
                {
                    DL.VentaProducto DLventaProducto = new DL.VentaProducto();
                    DLventaProducto.IdVenta = IdVenta;
                    DLventaProducto.IdSucursalProducto = IdSucursalProducto;
                    DLventaProducto.Cantidad = cantidad;
                    context.VentaProductos.Add(DLventaProducto);

                    int RowsAffected = context.SaveChanges();
                    if (RowsAffected > 0)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
