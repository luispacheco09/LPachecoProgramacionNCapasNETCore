using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        private readonly IConfiguration _configuration;

        public VentaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        //public IActionResult GetProducto(int IdDepartamento)
        //{
        //    bool sinStock = _configuration.GetValue<bool>("MostrarProductosSinStock");
        //    ML.SucursalProducto productoSuc = new ML.SucursalProducto();
        //    ML.Result resultSucursal = BL.SucursalProducto.GetProductbySucDepto(IdDepartamento);
        //    if (!sinStock)
        //    {

        //    }
        //    productoSuc.SucuralesProductos = resultSucursal.Objects;
        //    return PartialView("GetProductos", productoSuc);
        //}
        public JsonResult GetDepartamentosList(int IdArea)
        {
            ML.Result resultDepartamentos = BL.Departamento.GetByIdArea(IdArea);
            return Json(resultDepartamentos.Objects);
        }

        public IActionResult AddCart(int IdSucursalProducto)
        {
            var session = HttpContext.Session.GetString("Session");
            List<ML.VentaProducto> listaProductos;

            if (session == null)
            {
                // Si la sesión es nula se crea una nueva lista de productos
                listaProductos = new List<ML.VentaProducto>();
            }
            else
            {
                // Si la sesión existe, deserializa la lista de productos
                listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);

                // Verifica si el producto ya está en el carrito
                if (listaProductos.Any(vp => vp.SucursalProducto.IdSucursalProducto == IdSucursalProducto))
                {
                    return RedirectToAction("AumentarCantidad", "Venta", new { IdSucursalProducto = IdSucursalProducto, agregar = true });
                }
            }

            // se crea un nuevo objeto VentaProducto y se agrega a la lista
            ML.VentaProducto venta = new ML.VentaProducto();
            venta.SucursalProducto = new ML.SucursalProducto();
            venta.SucursalProducto.IdSucursalProducto = IdSucursalProducto;
            venta.Cantidad = 1;
            ML.Result resultProductos = BL.SucursalProducto.GetProductbySucProduct(IdSucursalProducto);
            //venta = (ML.VentaProducto)resultProductos.Object;
            venta.SucursalProducto = (ML.SucursalProducto)resultProductos.Object;
            listaProductos.Add(venta);

            // Guarda la lista actualizada en la sesión
            HttpContext.Session.SetString("Session", JsonSerializer.Serialize(listaProductos));

            return RedirectToAction("Cart", "Venta");
        }

        public IActionResult RemoveFromCart(int IdSucursalProducto)
        {
            var session = HttpContext.Session.GetString("Session");
            List<ML.VentaProducto> listaProductos;

            if (session != null)
            {
                listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);

                listaProductos.RemoveAll(vp => vp.SucursalProducto.IdSucursalProducto == IdSucursalProducto);
                // Guarda la lista actualizada en la sesión
                HttpContext.Session.SetString("Session", JsonSerializer.Serialize(listaProductos));
            }
            return RedirectToAction("Cart", "Venta");
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetString("Session");

            if (cart == null)
            {
                ViewData["Mensaje"] = "El carrito está vacío";
                return View(new List<ML.VentaProducto>());
            }

            List<ML.VentaProducto> carrito = JsonSerializer.Deserialize<List<ML.VentaProducto>>(cart);

            return View(carrito);
        }

        public IActionResult AumentarCantidad(int IdSucursalProducto, bool agregar)
        {
            var session = HttpContext.Session.GetString("Session");
            List<ML.VentaProducto> listaProductos;

            listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);
            var productoExistente = listaProductos.FirstOrDefault(vp => vp.SucursalProducto.IdSucursalProducto == IdSucursalProducto);

            if (productoExistente != null)
            {
                if (agregar && productoExistente.Cantidad < productoExistente.SucursalProducto.Stock)
                {
                    productoExistente.Cantidad++;
                }
                else if (!agregar && productoExistente.Cantidad > 0)
                {
                    productoExistente.Cantidad--;

                    if (!agregar && productoExistente.Cantidad <= 0)
                    {
                        return RedirectToAction("RemoveFromCart", "Venta", new { IdSucursalProducto = IdSucursalProducto });
                    }
                }

                HttpContext.Session.SetString("Session", JsonSerializer.Serialize(listaProductos));
                return RedirectToAction("Cart", "Venta");
            }
            return RedirectToAction("Cart", "Venta");

        }
        //public IActionResult AumentarCantidad(int IdSucursalProducto, bool agregar)
        //{
        //    var session = HttpContext.Session.GetString("Session");
        //    List<ML.VentaProducto> listaProductos;

        //    listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);
        //    var productoExistente = listaProductos.FirstOrDefault(vp => vp.SucursalProducto.IdSucursalProducto == IdSucursalProducto);

        //    if (productoExistente != null)
        //    {
        //        if (agregar && productoExistente.Cantidad < productoExistente.SucursalProducto.Stock)
        //        {
        //            productoExistente.Cantidad++;
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "No puede seleccionar más de lo que hay en stock";
        //        }

        //        HttpContext.Session.SetString("Session", JsonSerializer.Serialize(listaProductos));
        //        return RedirectToAction("Cart", "Venta");
        //    }
        //    return RedirectToAction("Cart", "Venta");

        //}
    }
}
