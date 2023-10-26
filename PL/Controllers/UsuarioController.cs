using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]

        public ActionResult GetAll()
        {

            Usuario usuario = BL.Usuario.GetAllUsuario();
            usuario.Usuarios = usuario.Objects;
            return View(usuario);
        }


        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            BL.Usuario usuario = new BL.Usuario();
            usuario.Rol = new BL.Rol();
            Rol resultRol = Rol.GetAllRol();
          

            if(IdUsuario != null)
            {
                Usuario result = BL.Usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (Usuario)result.Object;
                    usuario.Rol.Roles = resultRol.Objects;
                }
            }
            
                usuario.Rol.Roles = resultRol.Objects;
            
           
            return View(usuario);
        }


        [HttpPost]
        public ActionResult Form(Usuario usuario)
        {

            if(usuario.IdUsuario == 0)
            {
                usuario= Usuario.Add(usuario);
                if(usuario.Correct)
                {
                    ViewBag.Mensaje = "Usuario agregado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al agregar el usuario";
                }

            }
            else
            {
                usuario = BL.Usuario.Update(usuario);
                if (usuario.Correct)
                {
                    ViewBag.Mensaje = "Usuario actualizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar el usuario";
                }
            }
            return PartialView("Modal");
        }
       
        public ActionResult Delete(int IdUsuario)
        {
            Usuario usuario = Usuario.Delete(IdUsuario);
          
            if (usuario.Correct)
            {
                ViewBag.Mensaje = "Usuario eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar el usuario";
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            Usuario usuarioResult = BL.Usuario.GetByEmail(usuario);
            usuarioResult.Rol = new Rol();
            HttpContext.Session.GetString("Usuario");

            if (usuarioResult.Correct)
            {
                Usuario usuarioIngresado = (Usuario)usuario.Object;
               if(usuarioIngresado.Email == usuario.Email & usuarioIngresado.Passwords == usuario.Passwords)
                {
                    return RedirectToAction("GetAll", "Usuario");
                }
                else
                {
                    return RedirectToAction("Login", "Usuario");
                }
            }
            return View();
        }


    }
}
