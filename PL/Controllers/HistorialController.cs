using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HistorialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            ML.Venta ventas = new ML.Venta();
            ML.Result resultHistorial = BL.Historial.GetAll();
            if (resultHistorial.Correct)
            {
                ventas.Ventas = resultHistorial.Objects.ToList();
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al traer los registros";
            }

            return View(ventas);
        }

        public IActionResult GetProductoHistorial(int IdVenta)
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ML.Result resultHistorial = BL.Historial.GetProductoHistorial(IdVenta);
            if (resultHistorial.Correct)
            {
                ventaProducto.VentasProductos = resultHistorial.Objects.ToList();
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al traer los registros";
            }

            return View(ventaProducto);
        }
    }
}
