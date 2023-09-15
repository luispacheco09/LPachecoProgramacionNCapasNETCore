using System;
using System.Collections.Generic;
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
                    //DLVenta.IdMetodoPago = venta.IdMetodoPago;
                    //DLVenta.Fecha = venta.Fecha;
             
                    context.Venta.Add(DLVenta);

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

    }
}
