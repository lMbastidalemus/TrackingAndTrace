using Azure.Identity;
using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        public int IdUsuario { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int IdRol { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }
        public List<object> Objects{ get; set; }


        public static Usuario GetAllUsuario()
        {
             Usuario usuario = new Usuario();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"GetAllUsuario");
                    if(query != null ) { 
                    
                        usuario.Objects = new List<object>().ToList();
                        foreach( var item in query )
                        {

                        }
                    }
                }

            }catch(Exception ex) { 
            
            
            }

            return usuario;
        }

    }
}
