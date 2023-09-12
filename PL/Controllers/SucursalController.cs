using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class SucursalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            ML.Sucursal sucursales = new ML.Sucursal();
            ML.Result resultSucursal = BL.Sucursal.GetAll();
            if (resultSucursal.Correct)
            {
                sucursales.Sucursales = resultSucursal.Objects.ToList();
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al traer los registros de las sucursales" + resultSucursal.ErrorMessage;
            }

            return View(sucursales);
        }
        [HttpGet] //Mostrar formulario
        public IActionResult Form(int? IdSucursal)
        {
            ML.Sucursal sucursal = new ML.Sucursal();

            if (IdSucursal == null)
            {
                return View(sucursal);
            }
            else
            {
                ML.Result result = BL.Sucursal.GetById(IdSucursal.Value);

                sucursal.IdSucursal = ((ML.Sucursal)result.Object).IdSucursal;
                sucursal.Nombre = ((ML.Sucursal)result.Object).Nombre;
                sucursal.Calle = ((ML.Sucursal)result.Object).Calle;
                sucursal.NumeroInterior = ((ML.Sucursal)result.Object).NumeroInterior;
                sucursal.NumeroExterior = ((ML.Sucursal)result.Object).NumeroExterior;
                sucursal.CP = ((ML.Sucursal)result.Object).CP;
                sucursal.Colonia = ((ML.Sucursal)result.Object).Colonia;
                sucursal.Municipio = ((ML.Sucursal)result.Object).Municipio;
                sucursal.Estado = ((ML.Sucursal)result.Object).Estado;
                sucursal.PaginaWeb = ((ML.Sucursal)result.Object).PaginaWeb;
                
                return View(sucursal);

            }
        }
        [HttpPost] //Recibe datos del formulario
        public ActionResult Form(ML.Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                if (sucursal.IdSucursal == 0)//add
                {
                    ML.Result result = BL.Sucursal.Add(sucursal);//Trae la informacion directa desde el BL

                    if (result.Correct)
                    {
                        //mediante el ViewBag enviamos datos del controlador a la vista
                        ViewBag.Mensaje = "Se ha registrado correctamente la sucursal";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha registado correctamente la sucursal " + result.ErrorMessage;
                    }
                }
                else //update
                {

                    ML.Result result = BL.Sucursal.Update(sucursal);//Trae la informacion directa desde el BL

                    if (result.Correct)
                    {
                        //mediante el ViewBag enviamos datos del controlador a la vista
                        ViewBag.Mensaje = "Se ha actualizado correctamente la sucursal";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha podido actualizar conrrectamente la sucursal " + result.ErrorMessage;
                    }
                }
                return PartialView("Modal");
            }
            return View(sucursal);
        }

        public ActionResult Delete(int IdSucursal)
        {
            ML.Result result = BL.Sucursal.Delete(IdSucursal);//Trae la informacion directa desde el BL

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente la sucursal";
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido eliminar la sucursal. Error: " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }
    }
}
