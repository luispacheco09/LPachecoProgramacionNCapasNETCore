using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;


namespace PL.Controllers
{
    public class VentaController : Controller
    {
        private UserManager<IdentityUser> userManager;// sin esta estancia no se puede hacer un crud y apunta a la tabla rol (RolManager)
        public VentaController(UserManager<IdentityUser> userMgr) //contructor de la clase la inicializa, si no se inicializa no se tiene acceso a la base de datos
        {
            userManager = userMgr;
        }
        /*Traer configuracion del appsettings*/
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
            //se saca la lista de la sesion
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
                        UnitAmount = (long)(item.SucursalProducto.Producto.PrecioUnitario * 100),
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
                /*ID USUARIO ACTUAL*/
                string userId = userManager.GetUserId(User);

                //Extraer la sesion
                var cart = HttpContext.Session.GetString("Session");

                if (!string.IsNullOrEmpty(cart))
                {
                    List<ML.VentaProducto> carrito = JsonSerializer.Deserialize<List<ML.VentaProducto>>(cart);
                    decimal? precioTotal = 0;

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

                        venta.total = venta.Cantidad * venta.SucursalProducto.Producto.PrecioUnitario;
                        precioTotal += venta.total; // Agrega el subtotal al total
                    }
                    //@User.Identity?.Name
                    BL.Venta.Add( userId,precioTotal);
                    //return RedirectToAction("GetParametersEmail", "Email", new { listacarrito = carrito });

                    // Limpia la sesión después de restar el stock
                    //HttpContext.Session.Remove("Session");
                }

                var transaction = session.PaymentIntentId.ToString();
                /**/
                return RedirectToAction("Success", "Venta");


                //return View("Success");
            }

            return View("Failed");
        }
        public IActionResult EnviarCorreo()
        {
            return View();
        }
        public IActionResult Success()
        {
            try
            {
                string user = _configuration.GetValue<string>("Email:UserName");
                string password = _configuration.GetValue<string>("Email:PassWord");
                string host = _configuration.GetValue<string>("Email:Host");
                int port = int.Parse(_configuration.GetValue<string>("Email:Port"));
                string from = _configuration.GetValue<string>("Email:UserName");

                string body = System.IO.File.ReadAllText("C:\\Users\\digis\\Documents\\Luis Angel Pacheco Cruz\\LPachecoProgramacionNCapasNETCore\\PL\\wwwroot\\mail.html");


                string? nombre = "";
                string? descripcion = "";
                decimal? subtotal = 0;
                decimal? precioTotal = 0;
                int? cantidad = 0;
                string productosInfo = "";

                //se saca la lista de la sesion
                var cart = HttpContext.Session.GetString("Session");
                List<ML.VentaProducto> carritoList = JsonSerializer.Deserialize<List<ML.VentaProducto>>(cart);

                foreach (var producto in carritoList)
                {
                    nombre = producto.SucursalProducto.Producto.Nombre;
                    descripcion = producto.SucursalProducto.Producto.Descripcion;
                    cantidad = producto.Cantidad;
                    producto.total = producto.Cantidad * producto.SucursalProducto.Producto.PrecioUnitario;
                    subtotal += producto.total; // Agrega el subtotal al total

                    string productoHtml = $@"
                             <h3>Producto: {nombre}</h3>
                             <p>Descripción: {descripcion}</p>
                             <span>Cantidad: {cantidad}</span>
                             <p>Subtotal: {subtotal}</p>
                         ";
                    productosInfo += productoHtml;

                    // Agregar el subtotal al total
                    precioTotal += subtotal;
                }

                body = body.Replace("{{ProductosInfo}}", productosInfo);
                body = body.Replace("{{Nombre}}", nombre);
                body = body.Replace("{{Descripcion}}", descripcion);
                body = body.Replace("{{Cantidad}}", cantidad.ToString());
                body = body.Replace("{{Total}}", precioTotal.ToString());

                /*Mail*/
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, "Confirmación de compra");
                mail.To.Add("pacheco09angel@gmail.com");
                mail.Subject = "Asunto gracias pro su compra";
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                /*SMTP*/
                SmtpClient client = new SmtpClient();
                client.Host = host;
                client.Port = port;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                NetworkCredential credentials = new NetworkCredential(user, password);
                client.Credentials = credentials;
                client.Send(mail);
                ViewBag.Mensaje = "Se ha enviado un correo con la confimación de compra";

                HttpContext.Session.Remove("Session");

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View();
        }
        public IActionResult Failed()
        {
            return View();
        }

    }
}
