using BL;
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


        //public IActionResult AddCart(int IdProducto)
        //{
        //    List<int> carrito;
        //    var cart = HttpContext.Session.GetString("Session");
        //    if (cart == null)
        //    {
        //        carrito = new List<int>();
        //    }
        //    else
        //    {
        //        carrito = JsonSerializer.Deserialize<List<int>>(cart);
        //    }
        //    carrito.Add(IdProducto);
        //    HttpContext.Session.SetString("Session", JsonSerializer.Serialize(carrito));

        //    return RedirectToAction("Cart", "Venta");
        //}

        //tercero
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
                    return RedirectToAction("AumentarCantidad", "Venta");
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
        public IActionResult AumentarCantidad()
        {
            var cantidad = HttpContext.Session.GetString("Session");
            List<ML.VentaProducto> ventaProductos = new List<ML.VentaProducto>();
            foreach (var item in cantidad)
            {
                
            }
                return RedirectToAction("Cart", "Venta");
        }

        //segundo
        //public IActionResult AddCart(int IdSucursalProducto)
        //{
        //    List<ML.VentaProducto> listaProductos;

        //    var session = HttpContext.Session.GetString("Session");
        //    if (session == null)
        //    {

        //        //ML.VentaProducto venta = new ML.VentaProducto();

        //        //List<ML.VentaProducto> listaProductos = new List<ML.VentaProducto>();
        //        listaProductos = new List<ML.VentaProducto>();

        //        //listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);

        //        //venta.SucursalProducto = new ML.SucursalProducto();
        //        //venta.SucursalProducto.IdSucursalProducto = IdSucursalProducto;
        //        //venta.Cantidad = 1;
        //        //listaProductos.Add(venta);



        //        //var resultProductos = BL.SucursalProducto.GetbySucursal(IdSucursalProducto);

        //        //ventaProductos.Add(resultProductos);
        //    }
        //    else
        //    {
        //        //List<ML.VentaProducto>
        //        listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);

        //        //foreach (var item in listaProductos)
        //        //{
        //        //    ML.VentaProducto vp = new ML.VentaProducto();

        //        //    item.SucursalProducto = new ML.SucursalProducto();

        //        //    if (IdSucursalProducto == item.SucursalProducto.IdSucursalProducto)
        //        //    {
        //        //        return RedirectToAction("AgregarOtroProducto", "Venta");

        //        //    }
        //        //    vp.Cantidad = 1;
        //        //    vp.SucursalProducto = new ML.SucursalProducto();
        //        //    vp.SucursalProducto.IdSucursalProducto = IdSucursalProducto;
        //        //    listaProductos.Add(vp);
        //        //}
        //        //listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(carrito);

        //    }

        //    foreach (var item in listaProductos)
        //    {
        //        ML.VentaProducto vp = new ML.VentaProducto();

        //        item.SucursalProducto = new ML.SucursalProducto();

        //        if (IdSucursalProducto == item.SucursalProducto.IdSucursalProducto)
        //        {
        //            return RedirectToAction("AgregarOtroProducto", "Venta");

        //        }
        //        vp.Cantidad = 1;
        //        vp.SucursalProducto = new ML.SucursalProducto();
        //        vp.SucursalProducto.IdSucursalProducto = IdSucursalProducto;
        //        listaProductos.Add(vp);
        //    }
        //    //listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(carrito);
        //    HttpContext.Session.SetString("Session", JsonSerializer.Serialize(listaProductos));
        //    //pachecocruzluis@gmail.com
        //    //wall69@xñ4
        //    return RedirectToAction("Cart", "Venta");
        //}

        //public IActionResult AddCart(int IdSucursalProducto)
        //{
        //    List<ML.VentaProducto> listaProductos;

        //    var session = HttpContext.Session.GetString("Session");
        //    if (session == null)
        //    {

        //        ML.VentaProducto venta = new ML.VentaProducto();

        //        //List<ML.VentaProducto> listaProductos = new List<ML.VentaProducto>();
        //        listaProductos = new List<ML.VentaProducto>();

        //        //listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);

        //        venta.SucursalProducto = new ML.SucursalProducto();
        //        venta.SucursalProducto.IdSucursalProducto = IdSucursalProducto;
        //        venta.Cantidad = 1;
        //        listaProductos.Add(venta);



        //        //var resultProductos = BL.SucursalProducto.GetbySucursal(IdSucursalProducto);

        //        //ventaProductos.Add(resultProductos);
        //    }
        //    else
        //    {
        //        //List<ML.VentaProducto>
        //            listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(session);

        //        foreach (var item in listaProductos)
        //        {
        //            ML.VentaProducto vp = new ML.VentaProducto();

        //            item.SucursalProducto = new ML.SucursalProducto();

        //            if (IdSucursalProducto == item.SucursalProducto.IdSucursalProducto)
        //            {
        //                return RedirectToAction("AgregarOtroProducto", "Venta");

        //            }
        //            vp.Cantidad = 1;
        //            vp.SucursalProducto = new ML.SucursalProducto();
        //            vp.SucursalProducto.IdSucursalProducto = IdSucursalProducto;
        //            listaProductos.Add(vp);
        //        }
        //        //listaProductos = JsonSerializer.Deserialize<List<ML.VentaProducto>>(carrito);

        //    }

        //    HttpContext.Session.SetString("Session", JsonSerializer.Serialize(listaProductos));

        //    return RedirectToAction("Cart", "Venta");
        //}


        //primero
        //public IActionResult Cart()
        //{
        //    var cart = HttpContext.Session.GetString("Session");
        //    if (cart == null)
        //    {
        //        ViewData["Mensaje"] = "El carrito esta vacio";
        //        return View();
        //    }
        //    ML.VentaProducto vp = new ML.VentaProducto();

        //    List<ML.VentaProducto> carrito = JsonSerializer.Deserialize<List<ML.VentaProducto>>(cart);
        //    List<ML.VentaProducto> vps = new List<ML.VentaProducto>();
        //    ML.Producto producto = new ML.Producto();
        //    foreach (var item in carrito)
        //    {
        //        //ML.Result resultProductos = BL.Producto.GetById(id);
        //        //productos.Add(producto);
        //        vp.Cantidad = item.Cantidad;
        //        vp.SucursalProducto = new ML.SucursalProducto();
        //        item.SucursalProducto = new ML.SucursalProducto();

        //        vp.SucursalProducto.IdSucursalProducto = item.SucursalProducto.IdSucursalProducto;

        //        vps.Add(vp);
        //    }

        //    return View(vps);
        //}

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
