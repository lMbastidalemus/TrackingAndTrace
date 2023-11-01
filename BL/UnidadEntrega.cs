using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class UnidadEntrega
    {

        public int IdUnidad { get; set; }
        public string NumeroPlaca { get; set; }
        public int Modelo { get; set; }
        public string Marca { get; set; }
        public int  AnioFabricacion { get; set; }
        public List<object> Unidades { get; set; }

        public BL.EstatusUnidad EstatusUnidad { get; set; }
        public string Exception { get; set; }

        public List<object> Objects{ get; set; }
        public bool Correct { get; set; }

        public static UnidadEntrega GetAll()
        {
            UnidadEntrega unidadEntrega = new UnidadEntrega();
            try
            {
                using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
                {
                    var query = (from UnidadEntrega in context.UnidadEntregas
                                 join EstatusUnidad in context.EstatusUnidads on UnidadEntrega.IdEstatusUnidad equals EstatusUnidad.IdEstatus

                                 select new
                                 {
                                     IdUnidad = UnidadEntrega.IdUnidad,
                                     NumeroPlaca = UnidadEntrega.NumeroPlaca,
                                     Modelo = UnidadEntrega.Modelo,
                                     Marca = UnidadEntrega.Marca,
                                     AnioFabricacion = UnidadEntrega.AnioFabricacion,
                                     IdEstatusUnidad = UnidadEntrega.IdEstatusUnidad,
                                     Estatus = UnidadEntrega.IdEstatusUnidadNavigation.Estatus,
                                 });

                    unidadEntrega.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {

                        foreach (var obj in query)
                        {
                            UnidadEntrega unidadEntregaResult = new UnidadEntrega();
                            unidadEntregaResult.IdUnidad = obj.IdUnidad;
                            unidadEntregaResult.NumeroPlaca = obj.NumeroPlaca;
                            unidadEntregaResult.Modelo = obj.Modelo;
                            unidadEntregaResult.Marca = obj.Marca;
                            unidadEntregaResult.AnioFabricacion = obj.AnioFabricacion;
                            unidadEntregaResult.EstatusUnidad = new BL.EstatusUnidad();
                            unidadEntregaResult.EstatusUnidad.IdEstatus = obj.IdEstatusUnidad;
                            unidadEntregaResult.EstatusUnidad.Estatus = obj.Estatus;
                            unidadEntrega.Objects.Add(unidadEntregaResult);
                        }
                        unidadEntrega.Correct = true;
                    }
                    else
                    {
                        unidadEntrega.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                unidadEntrega.Exception = ex.Message;
            }

            return unidadEntrega; ;
        }


        
    }
}
