using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public int IdRol { get; set; }

        public  string Tipo { get; set; }

        public List<object> Roles { get; set; }
        public List<object> Objects{ get; set; }

        public bool Correct { get; set; }

        public static Rol GetAllRol()
        {
            Rol rol = new Rol();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new TrackingAndTraceNetCoreContext())
                {
                    var query = context.Rols.FromSqlRaw($"GetAllRol").ToList();
                    if (query != null)
                    {

                        rol.Objects = new List<object>();
                        foreach (var item in query)
                        {

                            rol.IdRol = item.IdRol;
                            rol.Tipo = item.Tipo;
                            rol.Objects.Add(item);
                        }
                        rol.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {

                rol.Correct = false;
               
            }

            return rol;
        }
    }
}
