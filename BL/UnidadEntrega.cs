using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UnidadEntrega
    {

        public int IdUnidad { get; set; }
        public string NumeroPlaca { get; set; }
        public int Modelo { get; set; }
        public string Marca { get; set; }
        public int AnioFabricacion { get; set; }
        public List<object> Unidades { get; set; }

        public BL.EstatusUnidad EstatusUnidad { get; set; }
        public string Exception { get; set; }

        public List<object> Objects { get; set; }
        public bool Correct { get; set; }
        public object? Object { get; set; }

        //public static UnidadEntrega GetAll()
        //{
        //    UnidadEntrega unidadEntrega = new UnidadEntrega();
        //    try
        //    {
        //        using (DL.TrackingAndTraceNetCoreContext context = new DL.TrackingAndTraceNetCoreContext())
        //        {
        //            var query = (from UnidadEntrega in context.UnidadEntregas
        //                         join EstatusUnidad in context.EstatusUnidads on UnidadEntrega.IdEstatusUnidad equals EstatusUnidad.IdEstatus

        //                         select new
        //                         {
        //                             IdUnidad = UnidadEntrega.IdUnidad,
        //                             NumeroPlaca = UnidadEntrega.NumeroPlaca,
        //                             Modelo = UnidadEntrega.Modelo,
        //                             Marca = UnidadEntrega.Marca,
        //                             AnioFabricacion = UnidadEntrega.AnioFabricacion,
        //                             IdEstatusUnidad = UnidadEntrega.IdEstatusUnidad,
        //                             Estatus = UnidadEntrega.IdEstatusUnidadNavigation.Estatus,
        //                         });

        //            unidadEntrega.Objects = new List<object>();
        //            if (query != null && query.ToList().Count > 0)
        //            {

        //                foreach (var obj in query)
        //                {
        //                    UnidadEntrega unidadEntregaResult = new UnidadEntrega();
        //                    unidadEntregaResult.IdUnidad = obj.IdUnidad;
        //                    unidadEntregaResult.NumeroPlaca = obj.NumeroPlaca;
        //                    unidadEntregaResult.Modelo = obj.Modelo;
        //                    unidadEntregaResult.Marca = obj.Marca;
        //                    unidadEntregaResult.AnioFabricacion = obj.AnioFabricacion;
        //                    unidadEntregaResult.EstatusUnidad = new BL.EstatusUnidad();
        //                    unidadEntregaResult.EstatusUnidad.IdEstatus = obj.IdEstatusUnidad;
        //                    unidadEntregaResult.EstatusUnidad.Estatus = obj.Estatus;
        //                    unidadEntrega.Objects.Add(unidadEntregaResult);
        //                }
        //                unidadEntrega.Correct = true;
        //            }
        //            else
        //            {
        //                unidadEntrega.Correct = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        unidadEntrega.Exception = ex.Message;
        //    }

        //    return unidadEntrega; ;
        //}

        public static UnidadEntrega GetAll()
        {
            UnidadEntrega result = new UnidadEntrega();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "GetAllUnidad";
                    SqlCommand cmd = new SqlCommand(query, context);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tableUnidad = new DataTable();
                    adapter.Fill(tableUnidad);

                    if (tableUnidad.Rows.Count > 0)
                    {
                        result.Objects = new List<object>().ToList();

                        foreach (DataRow row in tableUnidad.Rows)
                        {
                            UnidadEntrega unidadEntrega = new UnidadEntrega();
                            unidadEntrega.IdUnidad = int.Parse(row[0].ToString());
                            unidadEntrega.NumeroPlaca = row[1].ToString();
                            unidadEntrega.Modelo = int.Parse(row[2].ToString());
                            unidadEntrega.Marca = row[3].ToString();
                            unidadEntrega.AnioFabricacion = int.Parse(row[4].ToString());
                            unidadEntrega.EstatusUnidad = new EstatusUnidad();
                            unidadEntrega.EstatusUnidad.IdEstatus = int.Parse(row[5].ToString());
                            unidadEntrega.EstatusUnidad.Estatus = row[6].ToString();

                            result.Objects.Add(unidadEntrega);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }


        public static UnidadEntrega GetById(int IdUnidad)
        {
            UnidadEntrega result = new UnidadEntrega();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "GetByIdUnidad";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = IdUnidad;

                    cmd.Parameters.AddRange(collection);


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tableUnidad = new DataTable();
                    adapter.Fill(tableUnidad);

                    if (tableUnidad.Rows.Count > 0)
                    {
                        DataRow row = tableUnidad.Rows[0];
                        UnidadEntrega unidadEntrega = new UnidadEntrega();
                        unidadEntrega.IdUnidad = int.Parse(row[0].ToString());
                        unidadEntrega.NumeroPlaca = row[1].ToString();
                        unidadEntrega.Modelo = int.Parse(row[2].ToString());
                        unidadEntrega.Marca = row[3].ToString();
                        unidadEntrega.AnioFabricacion = int.Parse(row[4].ToString());
                        unidadEntrega.EstatusUnidad = new EstatusUnidad();
                        unidadEntrega.EstatusUnidad.IdEstatus = int.Parse(row[5].ToString());
                        unidadEntrega.EstatusUnidad.Estatus = row[6].ToString();
                        result.Object = unidadEntrega;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }


        public static UnidadEntrega Add(UnidadEntrega unidad)
        {
            UnidadEntrega result = new UnidadEntrega();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AddUnidad";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[5];
                    collection[0] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[0].Value = unidad.NumeroPlaca;

                    collection[1] = new SqlParameter("@Modelo", SqlDbType.Int);
                    collection[1].Value = unidad.Modelo;

                    collection[2] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[2].Value = unidad.Marca;

                    collection[3] = new SqlParameter("@AnioFabricacion", SqlDbType.Int);
                    collection[3].Value = unidad.AnioFabricacion;

                    collection[4] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Int);
                    collection[4].Value = unidad.EstatusUnidad.IdEstatus;

                 

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int filasAfectas = cmd.ExecuteNonQuery();
                    if(filasAfectas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
  
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }

        public static UnidadEntrega Update(UnidadEntrega unidad)
        {
            UnidadEntrega result = new UnidadEntrega();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UpdateUnidad";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[6];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = unidad.IdUnidad;

                    collection[1] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[1].Value = unidad.NumeroPlaca;

                    collection[2] = new SqlParameter("@Modelo", SqlDbType.Int);
                    collection[2].Value = unidad.Modelo;

                    collection[3] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[3].Value = unidad.Marca;

                    collection[4] = new SqlParameter("@AnioFabricacion", SqlDbType.Int);
                    collection[4].Value = unidad.AnioFabricacion;

                    collection[5] = new SqlParameter("@IdEstatusUnidad", SqlDbType.Int);
                    collection[5].Value = unidad.EstatusUnidad.IdEstatus;



                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int filasAfectas = cmd.ExecuteNonQuery();
                    if (filasAfectas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }

        public static UnidadEntrega Delete(int IdUnidad)
        {
            UnidadEntrega result = new UnidadEntrega();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "DeleteUnidad";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value =IdUnidad;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int filasAfectas = cmd.ExecuteNonQuery();
                    if (filasAfectas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }



    }
}
