namespace WebApi.Helpers
{
    public class AppSettings
    {
        //public string scheme { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }

        public string secret { get; set; }
        public int segundosVidaToken { get; set; }
        public string nombreAPI { get; set; }
        public string versionAPI { get; set; }
        public string cadenaConexionSQLServer { get; set; }


        public string procedureHeaders { get; set; }

        public string procedureLogin { get; set; }
        public string procedureContratos { get; set; }
        public string procedureContratoDetalle { get; set; }
        public string procedureCreditoDetalle { get; set; }
        public string procedureCarteraGrupos { get; set; }
        public string procedureConsultaCurp { get; set; }
        public string procedureConsultaDispNombreGrupo { get; set; }

        public string APIKeyName { get; set; }
        public string APIKeyValue { get; set; }

    }
}