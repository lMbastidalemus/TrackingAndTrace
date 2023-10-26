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
            Usuario usuario = new Usuario();
            usuario.Rol = new Rol();
            Rol resultRol = Rol.GetAllRol();
            usuario.Rol.Roles = resultRol.Objects;

            if(IdUsuario != null)
            {
                Usuario result = BL.Usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (Usuario)result.Object;
                    usuario.Rol.Roles = usuario.Rol.Objects;
                }
            }
            usuario.Rol.Roles = resultRol.Objects;
            return View(usuario);
        }


        [HttpPost]
        public ActionResult Form(BL.Usuario usuario)
        {

            if(usuario.IdUsuario == null)
            {
                usuario = BL.Usuario.Add(usuario);
                if(usuario.Correct)
                {
                    ViewBag.Mensaje = "Usuario agregado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al agregar el usuario";
                }
               
            }
            return PartialView("Modal");
        }
        [HttpDelete]
        public ActionResult Delete(int IdUsuario)
        {
            BL.Usuario usuario = BL.Usuario.Delete(IdUsuario);
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


    }
}
