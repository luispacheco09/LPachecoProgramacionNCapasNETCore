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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EnviarEmail()
        {
            return View();
        }
       
        /**/




        private void EnviarEmail(string pathHTML, string UserName, string Nombre, string activateURL, string emailTo)
        {

            ML.Result result = new ML.Result();

            try
            {

                result = BL.Email.PopulateBody(pathHTML, UserName, "", Nombre, activateURL);

                ML.Email emailModel = new ML.Email();

                emailModel.From =  _configuration.GetValue<string>("Email:UserName");// //web.config
                emailModel.FromDisplayName = "Gracias por su compra";// "Control de Asistencia Escolar";//web.config
                emailModel.Host = _configuration.GetValue<string>("Email:Host"); ; // //web.config               
                emailModel.User = _configuration.GetValue<string>("Email:UserName"); ; // //web.config
                emailModel.Password = _configuration.GetValue<string>("Email:PassWord"); ; // //web.config
                emailModel.Port = int.Parse(_configuration.GetValue<string>("Email:Port")); //;//web.config
                emailModel.Body = result.Object.ToString();
                emailModel.Subject = "Recuperación de contraseña";//web.config
                emailModel.To = emailTo;//Recuperar el correo de la BD
                
                                result = BL.Email.SendEmail(emailModel);

                if (result.Correct)
                {
                    Response.Redirect("~/Account/RecuperarPasswordSuccessEmail.aspx");
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




        //private void GetParametersEmail(string UserName, string Email, ML.UsuarioIdPlantel usuario)
        //{
        //    string Nombre;
        //    string recoverPasswordURL;
        //    //string pathHTML = Server.MapPath("~")+ConfigurationManager.AppSettings["pathHTML"];
        //    string pathHTML = Server.MapPath("~/Account/EmailRecoverPassword.html");


        //    if (usuario.RoleName == "ALUMNO")
        //    {
        //        Nombre = usuario.Alumno.Nombre + " " + usuario.Alumno.ApellidoPaterno + " " + usuario.Alumno.ApellidoMaterno;
        //    }
        //    else
        //        if (usuario.RoleName == "DOCENTE")
        //    {
        //        Nombre = usuario.Docente.Nombre + " " + usuario.Docente.ApellidoPaterno + " " + usuario.Docente.ApellidoMaterno;
        //    }
        //    else
        //    {
        //        Nombre = UserName;
        //    }

        //    recoverPasswordURL = ConfigurationManager.AppSettings["recoverPasswordURL"] + usuario.UserName;
        //    // InitializeControls();
        //    EnviarEmail(pathHTML, UserName, Nombre, recoverPasswordURL, Email);


        //}

    }
}
