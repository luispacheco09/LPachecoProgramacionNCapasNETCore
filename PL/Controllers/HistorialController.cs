using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Authorize(Roles = "User")]


    public class HistorialController : Controller
    {

        private UserManager<IdentityUser> userManager;// sin esta estancia no se puede hacer un crud y apunta a la tabla rol (RolManager)
        private readonly SignInManager<IdentityUser> _signInManager;

        public HistorialController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInManager) //contructor de la clase la inicializa, si no se inicializa no se tiene acceso a la base de datos
        {
            userManager = userMgr;
            _signInManager = signInManager;

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

        public async Task<IActionResult> GetProductoHistorial(int IdVenta)
        {
            string userId = userManager.GetUserId(User);

            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ML.Result resultHistorial = BL.Historial.GetProductoHistorial(IdVenta, userId);
          
            if (resultHistorial.Correct)
            {
                ventaProducto.VentasProductos = resultHistorial.Objects.ToList();
            }
            else
            {
                ViewBag.Message = "Para poder ver este detalle de compra, ingrese con su cuenta correcta por favor";
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index","Home");

            }

            return View(ventaProducto);
        }
    }
}
