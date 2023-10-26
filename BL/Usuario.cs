using Azure.Identity;
using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BL
{
    public class Usuario
    {

        public int IdUsuario { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public byte[] Passwords { get; set; }

        public BL.Rol Rol { get; set; }

        public string Email { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public List<object> Usuarios { get; set; }
        public List<object> Objects { get; set; }

        public bool Correct { get; set; }
        public object Object { get; set; }
        public string Exception { get; set; }

        public static Usuario GetAllUsuario()
        {
            Usuario usuario = new Usuario();

            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {
                    var query = context.Usuarios.FromSqlRaw("GetAllUsuario");
                    if (query != null)
                    {

                        usuario.Objects = new List<object>().ToList();
                        foreach (var item in query)
                        {
                            Usuario usuarioResult = new Usuario();
                            usuarioResult.IdUsuario = item.IdUsuario;
                            usuarioResult.UserName = item.UserName;
                            usuarioResult.Email = item.Email;
                            usuarioResult.Nombre = item.Nombre;
                            usuarioResult.ApellidoPaterno = item.ApellidoPaterno;
                            usuarioResult.ApellidoMaterno = item.ApellidoPaterno;
                            usuarioResult.Rol = new Rol();
                            usuarioResult.Rol.Tipo = item.Tipo;
                            usuario.Objects.Add(usuarioResult);


                        }
                        usuario.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {

                usuario.Correct = false;
                usuario.Exception = ex.Message;
            }

            return usuario;
        }


        public static Usuario GetById(int IdUsuario)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"GetByIdUsuario '{IdUsuario}'").AsEnumerable().SingleOrDefault();
                    if (query != null)
                    {
                        Usuario usuarioResult = new Usuario();
                        usuarioResult.UserName = query.UserName;
                        usuarioResult.Passwords = query.Password;
                        usuarioResult.Email = query.Email;
                        usuarioResult.Nombre = query.Nombre;
                        usuarioResult.ApellidoPaterno = query.ApellidoPaterno;
                        usuarioResult.ApellidoMaterno = query.ApellidoPaterno;
                        usuarioResult.Rol = new Rol();
                        usuarioResult.Rol.Tipo = query.Tipo;
                        usuario.Object = usuarioResult;

                        usuario.Correct = true;

                    }
                }

            }
            catch (Exception ex)
            {

                usuario.Correct = false;
                usuario.Exception = ex.Message;
            }

            return usuario;
        }


        public static Usuario Add(Usuario usuario)
        {
            string messageString = usuario.Password;

            //Convert the string into an array of bytes.
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);

            //Create the hash value from the array of bytes.
            usuario.Passwords = SHA256.HashData(messageBytes);
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"AddUsuario '{usuario.UserName}', @Password, '{usuario.Rol.IdRol}', '{usuario.Email}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'", new SqlParameter("@Password", usuario.Passwords));
                    if (query > 0)
                    {

                        usuario.Correct = true;

                    }
                    else
                    {
                        usuario.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {

                usuario.Correct = false;
                usuario.Exception = ex.Message;
            }

            return usuario;
        }

        public static Usuario Update(Usuario usuario)
        {
            string messageString = usuario.Password;

            //Convert the string into an array of bytes.
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);

            //Create the hash value from the array of bytes.
            usuario.Passwords = SHA256.HashData(messageBytes);
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateUsuario '{usuario.IdUsuario}', '{usuario.UserName}', @Password, '{usuario.Rol.IdRol}', '{usuario.Email}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'", new SqlParameter("@Password", usuario.Passwords));
                    if (query > 0)
                    {

                        usuario.Correct = true;

                    }
                    else
                    {
                        usuario.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {

                usuario.Correct = false;
                usuario.Exception = ex.Message;
            }

            return usuario;
        }

        public static Usuario Delete(int IdUsuario)
        {
            Usuario usuario = new Usuario();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteUsuario '{IdUsuario}'");
                    if (query > 0)
                    {

                        usuario.Correct = true;

                    }
                    else
                    {
                        usuario.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {

                usuario.Correct = false;
                usuario.Exception = ex.Message;
            }

            return usuario;
        }

        public static Usuario GetByEmail(Usuario usuario)
        {
            string messageString = usuario.Password;

            //Convert the string into an array of bytes.
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageString);

            //Create the hash value from the array of bytes.
            usuario.Passwords = SHA256.HashData(messageBytes);
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {

                    var item = context.Usuarios.FromSqlRaw($"GetByEmail '{usuario.Email}'").AsEnumerable().SingleOrDefault();
                    if (item != null)
                    {

                        Usuario usuarioResult = new Usuario();
                        usuarioResult.IdUsuario = item.IdUsuario;
                        usuarioResult.UserName = item.UserName;
                        usuarioResult.Passwords = usuario.Passwords;
                        usuarioResult.Rol = new Rol();
                        usuarioResult.Rol.IdRol = item.IdRol.Value;
                        usuarioResult.Rol.Tipo = item.Tipo;
                        usuarioResult.Email = item.Email;
                        usuarioResult.Nombre = item.Nombre;
                        usuarioResult.ApellidoPaterno = item.ApellidoPaterno;
                        usuarioResult.ApellidoMaterno = item.ApellidoMaterno;
                        usuario.Object = usuarioResult;

                        usuario.Correct = true;

                    }
                    else
                    {
                        usuario.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {

                usuario.Correct = false;
                usuario.Exception = ex.Message;
            }

            return usuario;
        }

    }
}
