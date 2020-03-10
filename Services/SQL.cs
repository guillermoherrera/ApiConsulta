using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace ApiConsulta.Services
{
    public class SQL
    {
        //public async Task<bool> GetFielsHeaders(Microsoft.AspNetCore.Http.HttpContext context)
        //{             
        //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(appSettings.cadenaConexionSQLServer))
        //    {
        //        System.Data.SqlClient.SqlParameter[] parameters =
        //        {
        //            new System.Data.SqlClient.SqlParameter("@apiKeyName", System.Data.SqlDbType.VarChar, 50) { Direction = System.Data.ParameterDirection.Output },
        //            new System.Data.SqlClient.SqlParameter("@apiKeyValue", System.Data.SqlDbType.VarChar, 50) { Direction = System.Data.ParameterDirection.Output }
        //        };

        //        await connection.OpenAsync();
        //        using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(appSettings.procedureHeaders, connection) { CommandType = System.Data.CommandType.StoredProcedure })
        //        {
        //            command.Parameters.AddRange(parameters);
        //            await command.ExecuteNonQueryAsync();

        //            if (parameters[0].Value == System.DBNull.Value)
        //                InternalServerError(context);

        //            if (parameters[1].Value == System.DBNull.Value)
        //                InternalServerError(context);

        //            APIKeyName = parameters[0].Value.ToString();
        //            APIKeyValue = parameters[1].Value.ToString();

        //            return true;
        //        }
        //    }
        //}

        //private void Unauthorized(HttpContext context)
        //{
        //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //    context.Response.WriteAsync("NO AUTORIZADO.");
        //}

        //private void InternalServerError(HttpContext context)
        //{
        //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //    context.Response.WriteAsync("NO AUTORIZADO.");
        //}
    }
}
