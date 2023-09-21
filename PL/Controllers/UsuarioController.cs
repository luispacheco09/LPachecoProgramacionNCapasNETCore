using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PL.Controllers
{
    [Authorize]

    public class UsuarioController : Controller
    {
        private UserManager<IdentityUser> userManager;// sin esta estancia no se puede hacer un crud y apunta a la tabla rol (RolManager)
        public UsuarioController(UserManager<IdentityUser> userMgr) //contructor de la clase la inicializa, si no se inicializa no se tiene acceso a la base de datos
        {
            userManager = userMgr;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario user = new ML.Usuario();
            user.Usuarios = new List<object>();

            var Usuarios = userManager.Users.ToList();
            return View(Usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Form([Required] Microsoft.AspNetCore.Identity.IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
     
                var userExists = await userManager.FindByIdAsync(user.Id);

                if (userExists == null)
                {
                    //Add
                    //result = await userManager.CreateAsync(new IdentityUser(user.UserName));
                    result = await userManager.CreateAsync(new IdentityUser(user.PhoneNumber));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                    }
                }
                else
                {
                    result = await userManager.UpdateAsync(userExists);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {

                    }
                }
            }
            return View(user);
        }


        public async Task<IActionResult> Form(Guid? IdUsuario)
        {
            if (IdUsuario == null)
            {
                return View(new IdentityUser());
            }
            else
            {
                var users = await userManager.FindByIdAsync(IdUsuario.ToString());
                return View(users);
            }
        }
    }
}
