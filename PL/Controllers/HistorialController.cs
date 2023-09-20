using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Authorize]

    public class HistorialController : Controller
    {

        private UserManager<IdentityUser> userManager;// sin esta estancia no se puede hacer un crud y apunta a la tabla rol (RolManager)
        public HistorialController(UserManager<IdentityUser> userMgr) //contructor de la clase la inicializa, si no se inicializa no se tiene acceso a la base de datos
        {
            userManager = userMgr;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            string userId = userManager.GetUserId(User);

            ML.Venta ventas = new ML.Venta();
            ML.Result resultHistorial = BL.Historial.GetAll(userId);
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
