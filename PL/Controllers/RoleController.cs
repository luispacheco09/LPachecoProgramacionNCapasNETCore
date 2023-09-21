using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ML;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PL.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;// sin esta estancia no se puede hacer un crud y apunta a la tabla rol (RolManager)
        public RoleController(RoleManager<IdentityRole> roleMgr) //contructor de la clase la inicializa, si no se inicializa no se tiene acceso a la base de datos
        {
            roleManager = roleMgr;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Role rol = new ML.Role();
            rol.Roles = new List<object>();

            var Roles = roleManager.Roles.ToList();
            return View(Roles);
        }

        //[HttpPost]
        //public async Task<IActionResult> Form([Required] Microsoft.AspNetCore.Identity.IdentityRole rol)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityResult result = await roleManager.CreateAsync(new IdentityRole(rol.Name));
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("GetAll");
        //        }
        //        else
        //        {

        //        }
        //        //  Errors(result);
        //    }
        //    return View(rol);
        //}

        [HttpPost]
        public async Task<IActionResult> Form([Required] Microsoft.AspNetCore.Identity.IdentityRole rol)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;

                //IdentityRole role = await roleManager.FindByIdAsync(rol.Id.ToString());
                var rolExists = await roleManager.FindByIdAsync(rol.Id);
                //var rolExists = roleManager.Roles.FirstOrDefault(r => r.Id == rol.Id);


                if (rolExists == null)
                {
                    //Add
                    result = await roleManager.CreateAsync(new IdentityRole(rol.Name));
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

                    //result = await roleManager.UpdateAsync(new IdentityRole(rol.Name));
                    //rolExists.Name = "Admin";
                    result = await roleManager.UpdateAsync(rolExists);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        
                    }
                }

                //if (rol.Id == "")//add
                //{
                //    result = await roleManager.CreateAsync(new IdentityRole(rol.Name));
                //    if (result.Succeeded)
                //    {
                //        return RedirectToAction("GetAll");
                //    }
                //    else
                //    {

                //    }
                //    //  Errors(result);
                //}
                //else//update
                //{
                //    result = await roleManager.UpdateAsync(rol);
                //    if (result.Succeeded)
                //    {
                //        return RedirectToAction("GetAll");
                //    }

                //}

            }
            return View(rol);
        }


        //public async Task<IActionResult> Form(Guid? IdRole)
        //{
        //    //IdentityRole rol = new IdentityRole();
        //    //role.Id = await roleManager.FindByIdAsync(IdRole.ToString());

        //    IdentityRole role = await roleManager.FindByIdAsync(IdRole.ToString());
        //    if (role == null)
        //    {
        //        return View(role);

        //    }
        //    else
        //    {
        //        var result = await roleManager.UpdateAsync(role);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("GetAll");
        //        }
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("No se pudo actualizar el rol", error.Description);
        //            }
        //        }
        //        return View(role);

        //    }
        //}

        //public IActionResult Form(Guid? IdRole)
        public async Task<IActionResult> Form(Guid? IdRole)
        {
            //IdentityRole rol = new IdentityRole();

            //IdentityRole role = roleManager.FindByIdAsync(IdRole.ToString()).Result;
            //IdentityRole role = await roleManager.FindByIdAsync(IdRole.ToString());
            //IdentityRole role = roleManager.FindByIdAsync(IdRole.ToString()).Result;

            if (IdRole == null) 
            {
                return View(new IdentityRole());
            }
            else
            {
                //IdentityRole roles = roleManager.Roles.FirstOrDefault(r => r.Id == IdRole.ToString());
                //var roles = roleManager.Roles.FirstOrDefault(r => r.Id == IdRole.ToString());

                var roles = await roleManager.FindByIdAsync(IdRole.ToString());
                return View(roles);

            }

        }


        public async Task<IActionResult> Delete(Guid? IdRole)
        {
            //IdentityRole rol = new IdentityRole();
             var role = await roleManager.FindByIdAsync(IdRole.ToString());
            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("No se pude eliminar el rol", error.Description);
                    }
                }
            }
            else
            {
                 
            }

            return View(role);
        }
    }
}


