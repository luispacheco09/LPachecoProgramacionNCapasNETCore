using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
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

            decimal? precioTotal = 0;
            // Calcular el subtotal para cada producto en el carrito
            foreach (var producto in carrito)
            {
                producto.total = producto.Cantidad * producto.SucursalProducto.Producto.PrecioUnitario;
                precioTotal += producto.total; // Agrega el subtotal al total
            }
            ViewBag.Total = precioTotal;
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
            //return PartialView("Modal");
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
        public IActionResult CheckOut()
        {
            var cart = HttpContext.Session.GetString("Session");
            List<ML.VentaProducto> carritoList = JsonSerializer.Deserialize<List<ML.VentaProducto>>(cart);


            var domain = "https://localhost:7121/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Venta/OrderConfirmation",
                CancelUrl = domain + "Venta/Failed",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = "pachecoluis@gmail.com"
            };

            foreach (var item in carritoList)
            {
                //item.SucursalProducto.Producto = new ML.Producto();

                var sessionListItems = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.SucursalProducto.Producto.PrecioUnitario*100),
                        Currency = "mxn",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.SucursalProducto.Producto.Nombre.ToString(),
                        }
                    },
                    Quantity = item.Cantidad
                };
                options.LineItems.Add(sessionListItems);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }

        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                var cart = HttpContext.Session.GetString("Session");

                if (!string.IsNullOrEmpty(cart))
                {
                    List<ML.VentaProducto> carrito = JsonSerializer.Deserialize<List<ML.VentaProducto>>(cart);

                    foreach (var venta in carrito)
                    {
                        // Restar la cantidad en el carrito del stock total
                        ML.Result resultProductos = BL.SucursalProducto.GetProductbySucProduct(venta.SucursalProducto.IdSucursalProducto);
                        ML.SucursalProducto producto = (ML.SucursalProducto)resultProductos.Object;

                        if (producto != null)
                        {
                            producto.Stock -= venta.Cantidad;
                            // Actualizar el stock en la base de datos
                            BL.SucursalProducto.UpdateStock(venta.SucursalProducto.IdSucursalProducto, producto.Stock);
                        }
                    }

                    // Limpia la sesión después de restar el stock
                    HttpContext.Session.Remove("Session");
                }

                var transaction = session.PaymentIntentId.ToString();

                return View("Success");
            }

            return View("Failed");
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Failed()
        {
            return View();
        }

    }
}
