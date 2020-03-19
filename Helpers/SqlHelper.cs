using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsulta.Helpers
{
    public class SqlHelper
    {
        public static DataTable ExecuteDataTable(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParams, int config_dt)
        {
            DataTable dt = new DataTable();
            switch(config_dt)
            {
                case 1:
                    dt.Columns.Add("primerNombre");
                    dt.Columns.Add("segundoNombre");
                    dt.Columns.Add("apellidoPaterno");
                    dt.Columns.Add("apellidoMaterno");
                    dt.Columns.Add("fechaNacimiento");
                    dt.Columns.Add("rfc");
                    dt.Columns.Add("sistema");
                    dt.Columns.Add("noCda");
                    dt.Columns.Add("importeTotal");
                    dt.Columns.Add("saldoActual");
                    dt.Columns.Add("saldoAtrazado");
                    dt.Columns.Add("diasAtrazo");
                    dt.Columns.Add("telefono");
                    break;
                default:
                    break;
            }

            SqlDataReader dr = ExecuteReader(conn, cmdType, cmdText, cmdParams);

            switch (config_dt) 
            {
                case 1:
                    while (dr.Read())
                    {
                        dt.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12]);
                    }
                    break;
                default:
                    break;
            }

            return dt;
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
        {
            SqlCommand cmd = conn.CreateCommand();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
            var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }

        private static void PrepareCommand(SqlCommand cmd ,SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
        {
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if(trans != null) 
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if(cmdParams != null)
            {
                AttachParameters(cmd, cmdParams);
            }
        }

        private static void AttachParameters(SqlCommand cmd, SqlParameter[] cmdParams)
        {
            foreach(SqlParameter p in cmdParams)
            {
                if((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }
                cmd.Parameters.Add(p);
            }
        }
    }
}
