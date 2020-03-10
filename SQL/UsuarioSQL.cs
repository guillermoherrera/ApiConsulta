using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace ApiConsulta
{
    public class UsuarioSQL
    {
        private readonly AppSettings _appSettings;
        public UsuarioSQL(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<Usuario> Login(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            {
                SqlParameter[] parameters =
                {
                    /*00*/new SqlParameter("@usuario", System.Data.SqlDbType.VarChar, 30) { Value = username },
                    /*01*/new SqlParameter("@password", System.Data.SqlDbType.VarChar, 30) { Value = password },
                    /*02*/new SqlParameter("@usuarioId", System.Data.SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output },
                    /*03*/new SqlParameter("@perfilId", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output },
                    /*04*/new SqlParameter("@perfilDesc", System.Data.SqlDbType.VarChar, 30) { Direction = System.Data.ParameterDirection.Output },
                    /*05*/new SqlParameter("@resultCode", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output },
                    /*06*/new SqlParameter("@resultDesc", System.Data.SqlDbType.VarChar, 300) { Direction = System.Data.ParameterDirection.Output }
                };

                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(_appSettings.procedureLogin, connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    command.Parameters.AddRange(parameters);
                    await command.ExecuteNonQueryAsync();

                    if ((parameters[5].Value == System.DBNull.Value ? 1 : (int)parameters[5].Value) == 0)
                    {
                        return new Usuario()
                        {
                            usuarioId = parameters[2].Value == System.DBNull.Value ? 0 : (Int64)parameters[2].Value,
                            perfilId = parameters[3].Value == System.DBNull.Value ? 1 : (int)parameters[3].Value,
                            perfilDesc = parameters[4].Value == System.DBNull.Value ? "OCURRIO UN PROBLEMA AL DEVOLVER EL VALOR..." : parameters[4].Value.ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}