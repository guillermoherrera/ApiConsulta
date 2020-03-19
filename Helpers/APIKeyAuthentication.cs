using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace ApiConsulta.Helpers
{
    public class APIKeyAuthentication
    {
        private string APIKeyName = string.Empty;
        private string APIKeyValue = string.Empty;
        private const string FirebaseUserId = Constants.FirebaseUserId;

        private RequestDelegate requestDelegate;
        private AppSettings appSettings;

        public APIKeyAuthentication(RequestDelegate requestDelegate, AppSettings appSettings)
        {
            this.requestDelegate = requestDelegate;
            this.appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!await GetFielsHeaders(context))
                InternalServerError(context);

            if (!context.Request.Headers.ContainsKey(APIKeyName))
                Unauthorized(context);

            var apiKey = context.Request.Headers[APIKeyName];
            if (!apiKey.ToString().Equals(APIKeyValue))
                Unauthorized(context);
            else
                await requestDelegate.Invoke(context);
        }

        private async Task<bool> GetFielsHeaders(HttpContext context)
        {
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(appSettings.cadenaConexionSQLServer))
            {
                System.Data.SqlClient.SqlParameter[] parameters =
                {
                    new System.Data.SqlClient.SqlParameter("@apiKeyName", System.Data.SqlDbType.VarChar, 50) { Direction = System.Data.ParameterDirection.Output },
                    new System.Data.SqlClient.SqlParameter("@apiKeyValue", System.Data.SqlDbType.VarChar, 250) { Direction = System.Data.ParameterDirection.Output },
                    new System.Data.SqlClient.SqlParameter("@apiName", appSettings.APIKeyName ) {SqlDbType =System.Data.SqlDbType.VarChar, Size = 50 , Direction = System.Data.ParameterDirection.Input }
                };

                await connection.OpenAsync();
                using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(appSettings.procedureHeaders, connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    command.Parameters.AddRange(parameters);
                    await command.ExecuteNonQueryAsync();

                    if (parameters[0].Value == System.DBNull.Value)
                        InternalServerError(context);

                    if (parameters[1].Value == System.DBNull.Value)
                        InternalServerError(context);

                    APIKeyName = Constants.ApiKey;
                    APIKeyValue = parameters[1].Value.ToString();

                    return true;
                }
            }
        }

        private void Unauthorized(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.WriteAsync("NO AUTORIZADO.");
        }

        private void InternalServerError(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.WriteAsync("NO AUTORIZADO.");
        }
    }

    public static class HandlerExtensions
    {
        public static IApplicationBuilder UseAPIValidation(this IApplicationBuilder builder, AppSettings appSettings)
        {
            return builder.UseMiddleware<APIKeyAuthentication>(appSettings);
        }
    }
}