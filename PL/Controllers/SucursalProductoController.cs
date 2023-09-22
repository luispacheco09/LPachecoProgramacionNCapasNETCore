using Microsoft.AspNetCore.Mvc;
using ML;
using PL.Data;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SucursalProductoController : Controller
    {
        private readonly IConfiguration _configuration;
        public SucursalProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            //ML.Sucursal sucursal = new ML.Sucursal();
            //ML.Result resultSucursal = BL.Sucursal.GetAll();
            //sucursal.Sucursales = resultSucursal.Objects;
            //return View(sucursal);
            ML.SucursalProducto sucursalP = new ML.SucursalProducto();
            sucursalP.Sucursal = new ML.Sucursal();

            ML.Result resultSucursal = BL.Sucursal.GetAll();
            sucursalP.Sucursal.Sucursales = resultSucursal.Objects;
            return View(sucursalP);
        }

        public IActionResult GetSucursal(int IdSucursal)
        {
            ML.SucursalProducto sucursalp = new ML.SucursalProducto();
            ML.Result resultSucursal = BL.SucursalProducto.GetbySucursal(IdSucursal);

            sucursalp.SucuralesProductos = resultSucursal.Objects;

            return PartialView("ResulltadoSucursalP", sucursalp);
        }
        public IActionResult UpdateStock(int idStock, int txtStock)
        {
            ML.Result resultStock = BL.SucursalProducto.UpdateStock(idStock, txtStock);
            if (resultStock.Correct)
            {
                //ViewBag.Mensaje = "Se ha actualizado correctamente el stock";
                return Json(new { success = true, message = "Se ha actualizado correctamente el stock", nuevoValor = resultStock.Object });
            }
            else
            {
                //ViewBag.Mensaje = "No se ha podido actualizado el stock. Error: " + resultStock.ErrorMessage;
                return Json(new { success = false, message = "No se ha podido actualizar correctamente el stock" });

            }

            //return PartialView("Modal");
        }
        public ActionResult Delete(int IdSucursalProducto)
        {
            ML.Result result = BL.SucursalProducto.Delete(IdSucursalProducto);//Trae la informacion directa desde el BL

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente el producto de la sucursal";
                ViewBag.SucProduct = IdSucursalProducto;

            }
            else
            {
                ViewBag.Mensaje = "No se ha podido eliminar el producto  de la sucursal. Error: " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }
    }
}
