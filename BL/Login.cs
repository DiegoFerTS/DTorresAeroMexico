using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Login
    {
        public static ML.Resultado UserLogin(ML.Usuario usuario)
        {
            ML.Resultado resultado = new ML.Resultado();

            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UsuarioGetByUserName";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
                    collection[0].Value = usuario.UserName;

                    command.Parameters.AddRange(collection);
                    command.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable tablaUsuario = new DataTable();

                    adapter.Fill(tablaUsuario);

                    if (tablaUsuario.Rows.Count > 0)
                    {
                        DataRow fila = tablaUsuario.Rows[0];

                        string passwordResult = fila[1].ToString();

                        if (usuario.Password == passwordResult)
                        {
                            resultado.Correct = true;
                        } else
                        {
                            resultado.Correct = false;
                            resultado.Message = "La contraseña es incorrecta";
                        }
                    } else
                    {
                        resultado.Correct = false;
                        resultado.Message = "No se encontro al usuario";
                    }
                }
            } catch (Exception ex) { 
                resultado.Message = ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }
    }
}
