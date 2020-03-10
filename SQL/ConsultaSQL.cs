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
                        consulta.rfc = dataRow[5].ToString();
                        consulta.creditosActivos = exec.Rows.Count;
                        consulta.creditos = new List<Credito>();
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
    }
}
