using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusUnidad
    {
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public List<object> Objects { get; set; }
        public List<object> EstatusUnidades { get; set; }

        public bool Correct { get; set; }
        public string Exception { get; set; }


        public static EstatusUnidad GetAll()
        {
            EstatusUnidad estatusUnidad = new EstatusUnidad();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
                {
                    var query = (from EstatusUnidad in context.EstatusUnidads
                                 select new
                                 {
                                     IdEstatus = EstatusUnidad.IdEstatus,
                                     Estatus = EstatusUnidad.Estatus
                                 });

                    estatusUnidad.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {

                        foreach (var obj in query)
                        {
                            EstatusUnidad estatusUnidadResult = new EstatusUnidad();
                            estatusUnidadResult.IdEstatus = obj.IdEstatus;
                            estatusUnidadResult.Estatus = obj.Estatus;
                            estatusUnidad.Objects.Add(estatusUnidadResult);
                        }
                        estatusUnidad.Correct = true;
                    }
                    else
                    {
                        estatusUnidad.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                estatusUnidad.Exception = ex.Message;
            }

            return estatusUnidad;
        }
    }
}
