using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Repartidor
    {
        public int IdRepartidor { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public BL.UnidadEntrega UnidadEntrega { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaIngreso { get; set; }

        public string Fotografia { get; set; }

        public bool Correct { get; set; }
        public List<object> Repartidores { get; set; }
        public string Exception { get; set; }
        public List<object> Objects { get; set; }
        public object Object { get; set; }

        public static Repartidor GetAll()
        {
            Repartidor repartidor = new Repartidor();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
                {
                    var query = (from Repartidor in context.Repartidors
                                 join UnidadEntrega in context.Repartidors on Repartidor.IdUnidad equals UnidadEntrega.IdUnidad
                                 select new
                                 {
                                     IdRepartidor = Repartidor.IdRepartidor,
                                     Nombre = Repartidor.Nombre,
                                     ApellidoPaterno = Repartidor.ApellidoPaterno,
                                     ApellidoMaterno = Repartidor.ApellidoMaterno,
                                     IdUnidad = Repartidor.IdUnidad,
                                     NumeroPlaca = Repartidor.IdUnidadNavigation.NumeroPlaca,
                                     FechaIngreso = Repartidor.FechaIngreso,
                                     //Fotografia = Repartidor.Fotografia
                                 });

                    repartidor.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {
                      
                        foreach (var obj in query)
                        {
                            Repartidor repartidorResult = new Repartidor();
                            repartidorResult.IdRepartidor = obj.IdRepartidor;
                            repartidorResult.Nombre = obj.Nombre;
                            repartidorResult.ApellidoPaterno = obj.ApellidoPaterno;
                            repartidorResult.ApellidoMaterno = obj.ApellidoMaterno;
                            repartidorResult.UnidadEntrega = new UnidadEntrega();
                            repartidorResult.UnidadEntrega.IdUnidad = obj.IdUnidad;
                            repartidorResult.UnidadEntrega.NumeroPlaca = obj.NumeroPlaca;
                          
                            repartidorResult.FechaIngreso = DateTime.Parse(obj.FechaIngreso.ToString());
                            //repartidorResult.Fotografia = obj.Fotografia;
                            repartidor.Objects.Add(repartidorResult);
                        }
                        repartidor.Correct = true;
                    }
                    else
                    {
                        repartidor.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                repartidor.Exception = ex.Message;
            }

            return repartidor; 
        }

        public static Repartidor GetById(int IdRepartidor)
        {
            Repartidor repartidor = new Repartidor();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
                {
                    var query = (from Repartidor in context.Repartidors
                                 join UnidadEntrega in context.Repartidors on Repartidor.IdUnidad equals UnidadEntrega.IdUnidad
                                 where Repartidor.IdRepartidor == IdRepartidor
                                 select new
                                 {
                                     IdRepartidor = Repartidor.IdRepartidor,
                                     Nombre = Repartidor.Nombre,
                                     ApellidoPaterno = Repartidor.ApellidoPaterno,
                                     ApellidoMaterno = Repartidor.ApellidoMaterno,
                                     IdUnidad = Repartidor.IdUnidad,
                                     NumeroPlaca = Repartidor.IdUnidadNavigation.NumeroPlaca,
                                     Telefono = Repartidor.Telefono,
                                     FechaIngreso = Repartidor.FechaIngreso,
                                     IdEstatusUnidad = Repartidor.IdUnidadNavigation.IdEstatusUnidad,
                                     Estatus = Repartidor.IdUnidadNavigation.IdEstatusUnidadNavigation.Estatus
                                     //Fotografia = Repartidor.Fotografia
                                 }).SingleOrDefault();

                    if (query != null)
                    {

                        Repartidor repartidorResult = new Repartidor();
                        repartidorResult.IdRepartidor = query.IdRepartidor;
                        repartidorResult.Nombre = query.Nombre;
                        repartidorResult.ApellidoPaterno = query.ApellidoPaterno;
                        repartidorResult.ApellidoMaterno = query.ApellidoMaterno;
                        repartidorResult.UnidadEntrega = new UnidadEntrega();
                        repartidorResult.UnidadEntrega.EstatusUnidad = new EstatusUnidad();
                        repartidorResult.UnidadEntrega.IdUnidad = query.IdUnidad;
                        repartidorResult.UnidadEntrega.NumeroPlaca = query.NumeroPlaca;
                        repartidorResult.Telefono = query.Telefono;
                        repartidorResult.FechaIngreso = DateTime.Parse(query.FechaIngreso.ToString());
                        repartidorResult.UnidadEntrega.EstatusUnidad.IdEstatus = query.IdEstatusUnidad;
                        repartidorResult.UnidadEntrega.EstatusUnidad.Estatus = query.Estatus;
                        //repartidorResult.Fotografia = query.Fotografia;
                        repartidor.Object  = repartidorResult;

                        repartidor.Correct = true;
                    }
                    else
                    {
                        repartidor.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                repartidor.Exception = ex.Message;
            }

            return repartidor; 
        }

        public static Repartidor Delete(int IdRepartidor)
        {
            Repartidor repartidor = new Repartidor();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
                {
                    var query = (from Repartidor in context.Repartidors
                                 where Repartidor.IdRepartidor == IdRepartidor
                                 select Repartidor).FirstOrDefault();
                    context.Repartidors.Remove(query);
                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        repartidor.Correct = true;
                    }
                    else
                    {
                        repartidor.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                repartidor.Exception = ex.Message;
            }

            return repartidor;
        }

        public static Repartidor Add(Repartidor repartidor)
        {

            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
                {
                    DL.Repartidor repartidorResult = new DL.Repartidor();

                    repartidorResult.IdRepartidor = repartidor.IdRepartidor;
                    repartidorResult.Nombre = repartidor.Nombre;
                    repartidorResult.ApellidoPaterno = repartidor.ApellidoPaterno;
                    repartidorResult.ApellidoMaterno = repartidor.ApellidoMaterno;
                    repartidorResult.IdUnidad = repartidor.UnidadEntrega.IdUnidad;
                    repartidorResult.Telefono = repartidor.Telefono;
                    repartidorResult.FechaIngreso = repartidor.FechaIngreso;
                    //repartidorResult.Fotografia = repartidor.Fotografia;
                    context.Repartidors.Add(repartidorResult);
                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        repartidor.Correct = true;
                    }
                    else
                    {
                        repartidor.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                repartidor.Exception = ex.Message;
            }

            return repartidor;
        }

        public static Repartidor Update(Repartidor repartidor)
        {

            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
                {
                    var query = (from Repartidor in context.Repartidors
                                 where Repartidor.IdRepartidor == repartidor.IdRepartidor
                                 select Repartidor).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdRepartidor = repartidor.IdRepartidor;
                        query.Nombre = repartidor.Nombre;
                        query.ApellidoPaterno = repartidor.ApellidoPaterno;
                        query.ApellidoMaterno = repartidor.ApellidoMaterno;
                      
                        query.IdUnidad = repartidor.UnidadEntrega.IdUnidad;
                        query.Telefono = repartidor.Telefono;
                        query.FechaIngreso = repartidor.FechaIngreso;
                        //query.Fotografia = repartidor.Fotografia;
                        context.SaveChanges();

                        repartidor.Correct = true;
                    }
                    else
                    {
                        repartidor.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                repartidor.Exception = ex.Message;
            }

            return repartidor;
        }
    }
}
