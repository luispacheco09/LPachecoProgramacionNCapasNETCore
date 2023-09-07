using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Shared : Controller
    {
        public IActionResult _LayoutDDL()
        {
            ML.SucursalProducto sucursalP = new ML.SucursalProducto();
            sucursalP.Sucursal = new ML.Sucursal();

            ML.Result resultSucursal = BL.Sucursal.GetAll();
            sucursalP.Sucursal.Sucursales = resultSucursal.Objects;
            return View(sucursalP);
        }
    }
}
