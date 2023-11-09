using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class VuelosPasajeros
    {
        public static ML.Resultado Add(List<ML.Vuelo> vuelos, List<ML.Pasajero> pasajeros)
        {
            ML.Resultado resultado = new ML.Resultado();

            if (vuelos.Count == pasajeros.Count)
            {

                int totalRegistros = vuelos.Count;
                int registrosRealizados = 0;

                for (int i = 0; i < totalRegistros; i++)
                {

                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                        {
                            string query = "VuelosPasajerosAdd";

                            SqlCommand command = new SqlCommand(query, conexion);

                            command.CommandType = CommandType.StoredProcedure;


                            SqlParameter[] collection = new SqlParameter[2];

                            collection[0] = new SqlParameter("@NumeroVuelo", SqlDbType.VarChar);
                            collection[0].Value = vuelos[i].NumeroVuelo;
                            collection[1] = new SqlParameter("@IdPasajero", SqlDbType.Int);
                            collection[1].Value = pasajeros[i].Id;

                            command.Parameters.AddRange(collection);

                            command.Connection.Open();

                            int filasAfectadas = command.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                resultado.Correct = true;
                                registrosRealizados++;
                            }
                            else
                            {
                                resultado.Correct = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        resultado.Correct = false;
                        resultado.Message = ex.Message;
                    }

                    resultado.Message = "Se registraron " + registrosRealizados + " de " + totalRegistros + ".";

                }

            } else
            {
                resultado.Correct = false;
                resultado.Message = "Faltaron datos por agregar.";
            }

            return resultado;
        }



        public static ML.Resultado GetAll(DateTime fechaInicio, DateTime fechaFin)
        {
            ML.Resultado resultado = new ML.Resultado();
            resultado.Objects = new List<object>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "VueloGetAll";

                    SqlCommand command = new SqlCommand(query, conexion);

                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[2];

                    collection[0] = new SqlParameter("@FechaInicio", SqlDbType.DateTime);
                    collection[0].Value = fechaInicio;
                    collection[1] = new SqlParameter("@FechaFin", SqlDbType.DateTime);
                    collection[1].Value = fechaFin;

                    command.Parameters.AddRange(collection);

                    command.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable tablaVuelo = new DataTable();

                    adapter.Fill(tablaVuelo);

                    if (tablaVuelo.Rows.Count > 0)
                    {
                        foreach (DataRow fila in tablaVuelo.Rows)
                        {
                            ML.Vuelo vuelo = new ML.Vuelo();

                            vuelo.NumeroVuelo = fila[0].ToString();
                            vuelo.Origen = fila[1].ToString();
                            vuelo.Destino = fila[2].ToString();
                            vuelo.FechaSalida = fila[3].ToString();

                            resultado.Objects.Add(vuelo);
                        }

                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.Message = "No se encontraron vuelos reservados.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.Message = ex.Message;
            }

            return resultado;
        }
    }
}
