using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ML;
using PL.Data;
using System.ComponentModel.DataAnnotations;


namespace PL.Controllers
{
    [Authorize(Roles ="Admin")]

    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductoController(ApplicationDbContext context) 
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            ML.Producto productos = new ML.Producto();

            ML.Result resultProducto = BL.Producto.GetAll();

            if (resultProducto.Correct)
            {
                productos.Productos = resultProducto.Objects.ToList();
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al traer los registros de los productos" + resultProducto.ErrorMessage;
            }

            return View(productos);
        }

        [HttpGet] //Mostrar formulario
        public IActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();

            //drop down list marca
            ML.Result resultMarca = BL.Marca.GetAll();
            producto.Marca = new ML.Marca();
            producto.Marca.Marcas = resultMarca.Objects;
            
            //drop down list proveedor
            ML.Result resultProveedor = BL.Proveedor.GetAll();
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.Proveedores = resultProveedor.Objects;

            //drop down list departamento
            ML.Result resultDepartamento = BL.Departamento.GetAll();
            producto.Departamento = new ML.Departamento();
            //producto.Departamento.Departamentos = resultDepartamento.Objects;//temporalDepartamento

            ////drop down list area
            ML.Result resultArea = BL.Area.GetAll();
            producto.Departamento.Area = new ML.Area();
            producto.Departamento.Area.Areas = resultArea.Objects;



            if (IdProducto == null)
            {
                return View(producto);
            }
            else
            {
                ML.Result result = BL.Producto.GetById(IdProducto.Value);

                producto.IdProducto = ((ML.Producto)result.Object).IdProducto;
                producto.Nombre = ((ML.Producto)result.Object).Nombre;
                producto.Descripcion = ((ML.Producto)result.Object).Descripcion;
                producto.FechaIngreso = ((ML.Producto)result.Object).FechaIngreso;
                producto.PrecioUnitario = ((ML.Producto)result.Object).PrecioUnitario;
                producto.CodigoBarras = ((ML.Producto)result.Object).CodigoBarras;
                producto.Imagen = ((ML.Producto)result.Object).Imagen;
                producto.Modelo = ((ML.Producto)result.Object).Modelo;

                //ddl se inicalizan arriba
                producto.Marca.IdMarca = ((ML.Producto)result.Object).Marca.IdMarca;
                producto.Proveedor.IdProveedor = ((ML.Producto)result.Object).Proveedor.IdProveedor;
                producto.Departamento.IdDepartamento = ((ML.Producto)result.Object).Departamento.IdDepartamento;

                //Trae el departamento correcto
                producto.Departamento.Departamentos = resultDepartamento.Objects;//temporalDepartamento

                //se inicializa arriba area
                int? IdArea = producto.Departamento.Area.IdArea = ((ML.Producto)result.Object).Departamento.Area.IdArea;//temporalArea
                producto.Departamento.Area.Areas = resultArea.Objects;
                ML.Result resultIdArea = BL.Departamento.GetByIdArea(IdArea);
                producto.Departamento.Departamentos = resultIdArea.Objects;

                return View(producto);

            }
        }

        [HttpPost] //Recibe datos del formulario
        public ActionResult Form(ML.Producto producto, IFormFile? imgProducto)
        //public ActionResult Form(ML.Producto producto)
        {

            if (imgProducto != null)//Aplica para ambos add y update
            {
                producto.Imagen = ConvertToBytes(imgProducto);//Se convierte la imagen a bytes

            }
            if (ModelState.IsValid)
            {
                if (producto.IdProducto == 0)//add
                {
                    ML.Result result = BL.Producto.Add(producto);//Trae la informacion directa desde el BL

                    if (result.Correct)
                    {
                        //mediante el ViewBag enviamos datos del controlador a la vista
                        ViewBag.Mensaje = "Se ha registrado correctamente el producto";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha registado conrrectamente el producto " + result.ErrorMessage;
                    }
                }
                else //update
                {

                    ML.Result result = BL.Producto.Update(producto);//Trae la informacion directa desde el BL

                    if (result.Correct)
                    {
                        //mediante el ViewBag enviamos datos del controlador a la vista
                        ViewBag.Mensaje = "Se ha actualizado correctamente el producto";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se ha podido actualizar conrrectamente el producto " + result.ErrorMessage;
                    }
                }
                return PartialView("Modal");
            }

            //drop down list proveedor
            ML.Result resultMarca = BL.Marca.GetAll();
            producto.Marca = new ML.Marca();
            producto.Marca.Marcas = resultMarca.Objects;
            //drop down list proveedor
            ML.Result resultProveedor = BL.Proveedor.GetAll();
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.Proveedores = resultProveedor.Objects;
            
            ML.Result resultDepartamentor = BL.Departamento.GetAll();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Departamentos = resultDepartamentor.Objects;

            ////drop down list departamento
            producto.Departamento = new ML.Departamento();

            //drop down list area
            ML.Result resultArea = BL.Area.GetAll();
            producto.Departamento.Area = new ML.Area();
            producto.Departamento.Area.Areas = resultArea.Objects;

            return View(producto);
        }

        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.Delete(IdProducto);//Trae la informacion directa desde el BL

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado correctamente el producto";
            }
            else
            {
                ViewBag.Mensaje = "No se ha podido eliminar el producto. Error: " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        /**Convertir img a bytes**/
        public byte[] ConvertToBytes(IFormFile image)
        {
        
            using var fileStream = image.OpenReadStream();

            byte[] imageBytes = new byte[fileStream.Length];
            fileStream.Read(imageBytes, 0, (int)fileStream.Length);

            return imageBytes;
        }
        //DDL
        public JsonResult GetDepartamentosList(int IdArea)
        {
            //BL- > Departamentos de determinada Area 
            ML.Result resultDepartamentos = BL.Departamento.GetByIdArea(IdArea);
            //crear un nuevo stored GrupoGetByIdPlantel -> DepartamentoGetByIdArea
            return Json(resultDepartamentos.Objects);
        }
    }
}
