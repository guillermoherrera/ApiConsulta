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
    public class RenovacionSQL
    {
        private readonly AppSettings _appSettings;

        public RenovacionSQL(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<List<Contrato>> RenovacionGrupos(string userID, string fechaInicio, String fechaFin)
        {
            //return new List<Contrato>() { new Contrato { contratoId = 1} };
            List<Contrato> contratos = new List<Contrato>();

            using (SqlConnection connection = new SqlConnection(_appSettings.cadenaConexionSQLServer))
            {
                SqlParameter[] Parameters =
                {
                    new SqlParameter("@userID", userID) { SqlDbType = SqlDbType.VarChar, Size = 150, Direction = ParameterDirection.Input },
                    new SqlParameter("@status", false) { SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input},
                    new SqlParameter("@dateFrom", Convert.ToDateTime(fechaInicio)) { SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input},
                    new SqlParameter("@dateTo", Convert.ToDateTime(fechaFin)) { SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input}
                };

                DataTable exec = Helpers.SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, _appSettings.procedureContratos, Parameters, 1);

                foreach (DataRow dataRow in exec.Rows)
                {
                    Contrato contrato = new Contrato
                    {
                        status = dataRow[0].ToString(),
                        fechaTermina = dataRow[1].ToString(),
                        nombreGeneral = dataRow[2].ToString(),
                        contratoId = int.Parse(dataRow[3].ToString())
                    };
                    contratos.Add(contrato);
                }
            }

            return contratos;
        }
    }
}
