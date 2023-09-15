using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace PL.Controllers
{
    public class EmailController : Controller
    {
        private readonly IConfiguration _configuration;
        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public IActionResult EnviarEmail()
        //{
        //    return View();
        //}

        /**/




        //private void EnviarEmail(string pathHTML, string UserName, string Nombre, string emailTo)
        private void EnviarEmail(string pathHTML, string nombre, string descripcion, int? cantidad, decimal? subtotal)
        {

            ML.Result result = new ML.Result();

            try
            {

                result = BL.Email.PopulateBody(pathHTML, nombre, descripcion, cantidad, subtotal);

                ML.Email emailModel = new ML.Email();

                emailModel.From = _configuration.GetValue<string>("Email:UserName");// //web.config
                emailModel.FromDisplayName = "Confirmación de compra";// "Control de Asistencia Escolar";//web.config
                emailModel.Host = _configuration.GetValue<string>("Email:Host"); ; // //web.config               
                emailModel.User = _configuration.GetValue<string>("Email:UserName"); ; // //web.config
                emailModel.Password = _configuration.GetValue<string>("Email:PassWord"); ; // //web.config
                emailModel.Port = int.Parse(_configuration.GetValue<string>("Email:Port")); //;//web.config
                emailModel.Body = result.Object.ToString();
                emailModel.Subject = "¡Gracias por su compra!";//web.config
                                                               //emailModel.To = emailTo;//Recuperar el correo de la BD
                emailModel.To = "pacheco09angel@gmail.com";//Recuperar el correo de la BD


                result = BL.Email.SendEmail(emailModel);

                if (result.Correct)
                {
                    Response.Redirect("");
                }
                else
                {
                    //lblTextRegistro.InnerText = "Ocurrió un error al generar la contraseña " + result.ErrorMessage;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showModal()", true);
                }


            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
        }




        //private void GetParametersEmail(string UserName, string Email, ML.Usuario usuario)
        private void GetParametersEmail(ML.VentaProducto producto)
        {
            string? nombre = "";
            string? descripcion = "";
            decimal? subtotal = 0;
            decimal? precioTotal = 0;
            int? cantidad = 0;
            string productosInfo = "";
            string pathHTML = System.IO.File.ReadAllText("C:\\Users\\USER\\Source\\Repos\\LPachecoProgramacionNCapasNETCore\\PL\\wwwroot\\mail.html");

            //Nombre = usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno;

            nombre = producto.SucursalProducto.Producto.Nombre;
            descripcion = producto.SucursalProducto.Producto.Descripcion;
            cantidad = producto.Cantidad;
            producto.total = producto.Cantidad * producto.SucursalProducto.Producto.PrecioUnitario;
            subtotal += producto.total; // Agrega el subtotal

            // InitializeControls();
            //EnviarEmail(pathHTML, UserName, Nombre, Email);
            EnviarEmail(pathHTML, nombre, descripcion, cantidad, subtotal);


        }

    }
}
