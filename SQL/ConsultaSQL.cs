using ApiConsulta.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace ApiConsulta.SQL
{
    public class ConsultaSQL
    {
        private readonly AppSettings _appSettings;

        public ConsultaSQL(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<CreditoDetalle> getCurp(string curp)
        {
            CreditoDetalle consulta = new CreditoDetalle();

            using (SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            {
                SqlParameter[] Parameters =
                {
                    new SqlParameter("@Curp", SqlDbType.VarChar, 18) { Value = curp }
                };

                DataTable exec = Helpers.SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, _appSettings.procedureConsultaCurp, Parameters, 1);
            
                foreach(DataRow dataRow in exec.Rows)
                {
                    if (consulta.creditos is null)
                    {
                        consulta.primerNombre = dataRow[0].ToString();
                        consulta.segundoNombre = dataRow[1].ToString();
                        consulta.apellidoPaterno = dataRow[2].ToString();
                        consulta.apellidoMaterno = dataRow[3].ToString();
                        consulta.fechaNacimiento = DateTime.Parse(dataRow[4].ToString());
                        //consulta.rfc = dataRow[5].ToString();
                        consulta.creditosActivos = exec.Rows.Count;
                        consulta.creditos = new List<Credito>();
                        consulta.telefono = dataRow[12].ToString();
                    }
                    
                    if (String.IsNullOrEmpty(consulta.rfc))
                    {
                        consulta.rfc = dataRow[5].ToString();
                    }

                    if (consulta.creditos != null)
                    {
                        Credito credito = new Credito();
                        credito.sistema = dataRow[6].ToString();
                        credito.noCda = int.Parse(dataRow[7].ToString());
                        credito.importeTotal = decimal.Parse(dataRow[8].ToString());
                        credito.saldoActual = decimal.Parse(dataRow[9].ToString());
                        credito.saldoAtrasado = decimal.Parse(dataRow[10].ToString());
                        credito.diasAtraso = int.Parse(dataRow[11].ToString());
                        consulta.creditos.Add(credito);
                    }
                }
                
            }

            return consulta;
        }

        public async Task<DispoibilidadNombre> getDispNombreGrupo(string nombreGrupo, int sistema)
        {
            DispoibilidadNombre consulta = new DispoibilidadNombre();

            using (SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@referencia", nombreGrupo) { SqlDbType = SqlDbType.VarChar, Size = 120, Direction = ParameterDirection.Input },
                    new SqlParameter("@sistema", sistema) { SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input },
                    new SqlParameter("@disponibilidad", SqlDbType.Int) { Direction = ParameterDirection.Output },
                    new SqlParameter("@opcion1", SqlDbType.VarChar, 120) { Direction = ParameterDirection.Output },
                    new SqlParameter("@opcion2", SqlDbType.VarChar, 120) { Direction = ParameterDirection.Output },
                    new SqlParameter("@opcion3", SqlDbType.VarChar, 120) { Direction = ParameterDirection.Output },
                };

                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(_appSettings.procedureConsultaDispNombreGrupo, connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddRange(parameters);
                    await command.ExecuteNonQueryAsync();

                    //if (parameters[0].Value == DBNull.Value)
                    consulta = new DispoibilidadNombre
                    {
                        disponibilidad = parameters[2].Value.ToString() == "1" ? true : false,
                        opcion1 = parameters[3].Value.ToString(),
                        opcion2 = parameters[4].Value.ToString(),
                        opcion3 = parameters[5].Value.ToString(),
                    };
                }
            }

            return consulta;
        }
    }
}
