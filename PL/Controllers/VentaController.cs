using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult GetAllProducto()
        {
            ML.Producto producto = new ML.Producto();

            ML.Result resultDepartamento = BL.Departamento.GetAll();
            producto.Departamento = new ML.Departamento();

            ML.Result resultArea = BL.Area.GetAll();
            producto.Departamento.Area = new ML.Area();
            producto.Departamento.Area.Areas = resultArea.Objects;
           
            return View(producto);
        }
        public IActionResult GetProducto(int IdDepartamento)
        {
            ML.SucursalProducto productoSuc = new ML.SucursalProducto();
            ML.Result resultSucursal = BL.SucursalProducto.GetProductbySucDepto(IdDepartamento);
            productoSuc.SucuralesProductos = resultSucursal.Objects;
            return PartialView("GetProductos", productoSuc);
        }
        public JsonResult GetDepartamentosList(int IdArea)
        {
            ML.Result resultDepartamentos = BL.Departamento.GetByIdArea(IdArea);
            return Json(resultDepartamentos.Objects);
        }

        public IActionResult AddCart(int IdProducto)
        {
            List<int> carrito;
            var cart = HttpContext.Session.GetString("Session");
            if (cart == null)
            {
                carrito = new List<int>();
            }
            else
            {
                carrito = JsonSerializer.Deserialize<List<int>>(cart);
            }
            carrito.Add(IdProducto);
            HttpContext.Session.SetString("Session", JsonSerializer.Serialize(carrito));

            return RedirectToAction("Cart", "Venta");
        }

        //public IActionResult Cart()
        //{
        //    var cart = HttpContext.Session.GetString("Session");
        //    if (cart == null)
        //    {
        //        ViewData["Mensaje"] = "El carrito esta vacio";
        //        return View();
        //    }

        //     List<int> carrito = JsonSerializer.Deserialize<List<int>>(cart);
        //    List<ML.Producto> productos = new List<ML.Producto>();
        //    //ML.Producto resultProductos = new ML.Producto();
        //    foreach (int id in carrito)
        //    {
        //        ML.Result producto = BL.Producto.GetById(id);
        //        //productos.Add(producto);
        //         //resultProductos.Productos = producto.Objects.ToList();
        //        //productos.AddRange(producto.Objects);

        //    }
           
        //    return View();
        //}
    }
}
