using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class SucursalProductoController : Controller
    {
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
            ML.Sucursal sucursal = new ML.Sucursal();
            ML.Result resultSucursal = BL.SucursalProducto.GetbySucursal(IdSucursal);
            sucursal.Sucursales = resultSucursal.Objects;
            return PartialView("ResultadoSucursalP", resultSucursal);
        }
    }
}
